using BookstoreApp.MVVM.Views.LoginPages;
using BookstoreApp.MVVM.Views.NavigationPages;
using BookstoreClassLibrary.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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

        public IRelayCommand LoginCommand { get; }
        public IRelayCommand ToRegisterPageCommand { get; }
        public IRelayCommand SignUpCommand { get; }

        public UserViewModel (HttpClient httpClient)
        {
            _httpClient = httpClient;
            LoginCommand = new RelayCommand<User>(LoginCommandAction);
            ToRegisterPageCommand = new RelayCommand(ToRegisterPageCommandAction);
            SignUpCommand = new RelayCommand<User>(SignUpCommandAction);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<User>>($"{BaseUrl}/all");
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<User>($"{BaseUrl}/{id}");
        }

        public async Task<User> LoginAsync(string email, string password)
        {
            var user = new User { Email = email, Password = password };
            var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}/login", user);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<User>();
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Login failed", "Invalid username or password.", "OK");
            }
            return null;
        }

        public async Task<bool> AddUserAsync(User user)
        {
            var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}/add", user);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            var response = await _httpClient.PutAsJsonAsync($"{BaseUrl}/{user.Id}", user);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/{id}");
            return response.IsSuccessStatusCode;
        }

        public async void LoginCommandAction(User user)
        {
            if (user != null)
            {
                var response = LoginAsync(user.Email, user.Password);
                if (response != null)
                {
                    await SecureStorage.SetAsync("IsLoggedIn", "true");
                    await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Login failed", "Invalid username or password.", "OK");
            }
        }

        public async void ToRegisterPageCommandAction()
        {
            await Shell.Current.GoToAsync($"{nameof(RegistrationPage)}");
        }

        public async void SignUpCommandAction(User user)
        {
            if (user != null)
            {
                var response = await AddUserAsync(user);
                await Shell.Current.GoToAsync($"..");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Invalid username or password.", "OK");
            }
        }
    }
}
