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
        public string Street { get; set; }
        public string AddInfo { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Province { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public bool IsPrimary { get; set; }
        public List<ClientViewModel> Clients { get; set; }

        public AddressViewModel (HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<AddressViewModel>> GetAllAddressesAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<AddressViewModel>>($"{BaseUrl}/all");
        }

        public async Task<AddressViewModel> GetAddressByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<AddressViewModel>($"{BaseUrl}/{id}");
        }

        public async Task<bool> UpdateAddressAsync(AddressViewModel address)
        {
            if (address == null)
            {
                return false;
            }
            var response = await _httpClient.PutAsJsonAsync($"{BaseUrl}/{address.Id}", address);
            return response.IsSuccessStatusCode;
        }
    }
}
