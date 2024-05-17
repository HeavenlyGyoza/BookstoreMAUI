using BookstoreClassLibrary.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore_MAUI.MVVM.ViewModels
{
    public partial class AddressViewModel : ObservableObject
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://localhost:7299/Address";

        public int Id { get; set; }
        [ObservableProperty]
        public string treet;
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
        public List<Client> Clients { get; set; }

        public AddressViewModel (HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Address>> GetAllAddressesAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Address>>($"{BaseUrl}/all");
        }

        public async Task<Address> GetAddressByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Address>($"{BaseUrl}/{id}");
        }

        public async Task<bool> AddAddressAsync(Address address)
        {
            var response = await _httpClient.PatchAsJsonAsync($"{BaseUrl}/add", address);
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

        public async Task<bool> DeleteAuthorAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
