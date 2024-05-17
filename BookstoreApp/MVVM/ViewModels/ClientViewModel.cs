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
    public partial class ClientViewModel : ObservableObject
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://localhost:7299/Client";

        public int Id { get; set; }
        [ObservableProperty]
        public string name;
        [ObservableProperty]
        public string surname;
        [ObservableProperty]
        public string email;
        [ObservableProperty]
        public string phone;
        [ObservableProperty]
        public List<Address> addresses;
        
        public ClientViewModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Client>> GetAllClientsAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Client>>($"{BaseUrl}/all");
        }

        public async Task<Client> GetClientByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Client>($"{BaseUrl}/{id}");
        }

        public async Task<bool> AddClientAsync(Client client)
        {
            var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}/add", client);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateClientAsync(Client client)
        {
            var response = await _httpClient.PutAsJsonAsync($"{BaseUrl}/{client.Id}", client);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteClientAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
