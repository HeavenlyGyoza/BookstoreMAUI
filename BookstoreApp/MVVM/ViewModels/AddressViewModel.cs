﻿using BookstoreApp.MVVM.Views.UserPages;
using BookstoreClassLibrary.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Bookstore_MAUI.MVVM.ViewModels
{
    [QueryProperty(nameof(SelectedAddress), nameof(SelectedAddress))]
    public partial class AddressViewModel : ObservableObject
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://localhost:7299/Address";

        public int Id { get; set; }
        [ObservableProperty]
        public string street;
        [ObservableProperty]
        public string addInfo;
        [ObservableProperty]
        public string city;
        [ObservableProperty]
        public string postalCode;
        [ObservableProperty]
        public string province;
        [ObservableProperty]
        public string state;
        [ObservableProperty]
        public string country;
        [ObservableProperty]
        public bool isPrimary;
        [ObservableProperty]
        public Address selectedAddress;
        [ObservableProperty]
        public List<Client> clients;
        public Client client { get; set; }

        public ObservableCollection<Address> ClientAddresses { get; set; } = [];

        public IRelayCommand EditAddressCommand { get; }
        public IRelayCommand DeleteAddressCommand { get; }
        public IRelayCommand AddNewAddressCommand { get; }
        public IRelayCommand SaveAddressCommand { get; }

        public AddressViewModel (HttpClient httpClient)
        {
            _httpClient = httpClient;
            EditAddressCommand = new RelayCommand<Address>(EditAddressCommandAction);
            DeleteAddressCommand = new RelayCommand<Address>(DeleteAddressCommandAction);
            AddNewAddressCommand = new RelayCommand(AddNewAddressCommandAction);
            SaveAddressCommand = new RelayCommand<Client>(SaveAddressCommandAction);
            SelectedAddress = new Address();
            Clients = new List<Client>();
        }

        public async Task<IEnumerable<Address>> GetAllAddressesAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Address>>($"{BaseUrl}/all");
        }

        public async Task<Address> GetAddressByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Address>($"{BaseUrl}/{id}");
        }

        public async Task<Address?> GetPrimaryAddressByClientIdAsync(int clientId)
        {
            var addresses = await _httpClient.GetFromJsonAsync<IEnumerable<Address>>($"{BaseUrl}/byClientId/{clientId}");
            return addresses?.FirstOrDefault(a => a.IsPrimary);
        }

        public async Task<IEnumerable<Address>> GetAllAddressByClientIdAsync(int clientId)
        {
            var addresses = await _httpClient.GetFromJsonAsync<IEnumerable<Address>>($"{BaseUrl}/byClientId/{clientId}");
            return addresses ?? [];
        }

        public async Task<bool> AddAddressAsync(Address address)
        {
            var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}/add", address);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAddressAsync(Address address)
        {
            if (address == null)
            {
                return false;
            }
            var response = await _httpClient.PutAsJsonAsync($"{BaseUrl}/{address.Id}", address);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAddressAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task LoadClientAddressesCollection(int clientId)
        {
            ClientAddresses.Clear();
            var addresses = await GetAllAddressByClientIdAsync(clientId);
            foreach (var address in addresses)
            {
                ClientAddresses.Add(address);
            }
        }

        public async void EditAddressCommandAction(Address address)
        {
            SelectedAddress = address;
            await Shell.Current.GoToAsync(nameof(ManageUserAddressPage), new Dictionary<string, object>
            {
                {"SelectedAddress", address }
            });
        }

        public async void DeleteAddressCommandAction(Address address)
        {
            if (await Application.Current.MainPage.DisplayAlert("Success", "Book updated succesfully.", "Yes", "No"))
            {
                var response = await DeleteAddressAsync(address.Id);
                if (response)
                {
                    ClientAddresses.Remove(address);
                }
            }
        }

        public async void AddNewAddressCommandAction()
        {
            await Shell.Current.GoToAsync(nameof(ManageUserAddressPage));
        }

        public async void SaveAddressCommandAction(Client clientA)
        {
            if(SelectedAddress != null && SelectedAddress.Id > 0)
            {
                var response = await UpdateAddressAsync(SelectedAddress);
                if (response)
                {
                    await Shell.Current.GoToAsync("..");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Failed to update address.", "OK");
                }
            }
            else
            {
                SelectedAddress.Clients.Add(client);
                var response = await AddAddressAsync(SelectedAddress);
                if (response)
                {
                    await Shell.Current.GoToAsync("..");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Failed to add address.", "OK");
                }
            }
        }
    }
}
