﻿using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore_MAUI.MVVM.ViewModels
{
    public partial class UserViewModel : ObservableObject
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://localhost:7299/User";

        public int Id { get; set; }
        [ObservableProperty]
        public string email;
        [ObservableProperty]
        public string password;
        [ObservableProperty]
        public string role;

        public UserViewModel (HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<UserViewModel>> GetAllUsersAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<UserViewModel>>($"{BaseUrl}/all");
        }

        public async Task<UserViewModel> GetUserByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<UserViewModel>($"{BaseUrl}/{id}");
        }

        public async Task<bool> AddUserAsync(UserViewModel user)
        {
            var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}/add", user);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateUserAsync(UserViewModel user)
        {
            var response = await _httpClient.PutAsJsonAsync($"{BaseUrl}/{user.Id}", user);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
