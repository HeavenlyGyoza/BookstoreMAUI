﻿using BookstoreClassLibrary.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Bookstore_MAUI.MVVM.ViewModels
{
    [QueryProperty(nameof(SelectedBook), nameof(SelectedBook))]
    public partial class OrderViewModel : ObservableObject
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://localhost:7299/Order";
        private const string BookControllerUrl = "https://localhost:7299/Book";

        public int Id { get; set; }
        [ObservableProperty]
        public int quantity;
        [ObservableProperty]
        public decimal price;
        [ObservableProperty]
        public DateOnly orderDate;
        [ObservableProperty]
        public Client client;
        [ObservableProperty]
        public Address address;
        [ObservableProperty]
        public Book selectedBook;

        public IAsyncRelayCommand PlaceOrderCommand { get; }

        public OrderViewModel (HttpClient httpClient)
        {
            _httpClient = httpClient;
            PlaceOrderCommand = new AsyncRelayCommand(PlaceOrderCommandAction);
            address = new Address();
        }

        public async Task<IEnumerable<Order>> GettAllOrdersAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Order>>($"{BaseUrl}/all");
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Order>($"{BaseUrl}({id}");
        }

        public async Task<bool> AddOrderAsync(Order order)
        {
            var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}/add", order);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateOrderAsync(Order order)
        {
            var response = await _httpClient.PutAsJsonAsync($"{BaseUrl}/{order.Id}", order);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/{id}");
            return response.IsSuccessStatusCode;
        }

        private async Task PlaceOrderCommandAction()
        {
            var order = new Order
            {
                Quantity = Quantity,
                Price = SelectedBook.Price * Quantity,
                OrderDate = DateOnly.FromDateTime(DateTime.Now),
                ClientId = Client.Id,
                Client = Client,
                AddressId = Address.Id,
                Address = Address,
                BookId = SelectedBook.Id,
                Book = SelectedBook
            };
            var json = JsonSerializer.Serialize(order);
            var response = await AddOrderAsync(order);
            if (response)
            {
                await Application.Current.MainPage.DisplayAlert("Success", "Order placed successfully.", "OK");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Failed to place order.", "OK");
            }
        }

        public void UpdateTotalPrice()
        {
            Price = SelectedBook.Price * Quantity;
        }
    }
}
