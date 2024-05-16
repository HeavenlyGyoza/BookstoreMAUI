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
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public List<AddressViewModel> Addresses { get; set; }
        
        public ClientViewModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<ClientViewModel>> GetAllClientsAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<ClientViewModel>>($"{BaseUrl}/all");
        }

        public async Task<ClientViewModel> GetClientByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<ClientViewModel>($"{BaseUrl}/{id}");
        }

        public async Task<bool> AddClientAsync(ClientViewModel client)
        {
            var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}/add", client);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateClientAsync(ClientViewModel client)
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
