using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore_MAUI.MVVM.ViewModels
{
    public partial class OrderViewModel : ObservableObject
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://localhost:7299/Order";

        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateOnly OrderDate { get; set; }
        public ClientViewModel Client { get; set; }
        public AddressViewModel Address { get; set; }

        public OrderViewModel (HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<OrderViewModel>> GettAllOrdersAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<OrderViewModel>>($"{BaseUrl}/all");
        }

        public async Task<OrderViewModel> GetOrderByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<OrderViewModel>($"{BaseUrl}({id}");
        }

        public async Task<bool> AddOrderAsync(OrderViewModel order)
        {
            var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}/{order.Id}", order);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateOrderAsync(OrderViewModel order)
        {
            var response = await _httpClient.PutAsJsonAsync($"{BaseUrl}/{order.Id}", order);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
