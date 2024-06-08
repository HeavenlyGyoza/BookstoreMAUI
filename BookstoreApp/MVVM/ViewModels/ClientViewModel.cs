using BookstoreApp.MVVM.Views.LoginPages;
using BookstoreApp.MVVM.Views.NavigationPages;
using BookstoreApp.MVVM.Views.UserPages;
using BookstoreClassLibrary.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
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
        public string password;
        [ObservableProperty]
        public string role;
        [ObservableProperty]
        public string phone;
        [ObservableProperty]
        public List<Address> addresses;
        public CultureInfo lang;

        public IRelayCommand LoginCommand { get; }
        public IRelayCommand ToRegisterPageCommand { get; }
        public IRelayCommand ToLoginPageCommand { get; }
        public IRelayCommand ToUserOrderHistoryPageCommand { get; }
        public IRelayCommand ToAccountSettingsPageCommand { get; }
        public IRelayCommand ToUserAddressesPageCommand { get; }
        public IRelayCommand ToUserWishlistsPageCommand { get; }
        public IRelayCommand SignUpCommand { get; }
        public IRelayCommand LogOutCommand { get; }

        public ClientViewModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
            LoginCommand = new RelayCommand(LoginCommandAction);
            ToRegisterPageCommand = new RelayCommand(ToRegisterPageCommandAction);
            SignUpCommand = new RelayCommand(SignUpCommandAction);
            LogOutCommand = new RelayCommand(LogOutCommandAction);
            ToLoginPageCommand = new RelayCommand(ToLoginPageCommandAction);
            ToUserOrderHistoryPageCommand = new RelayCommand(ToUserOrderHistoryPageCommandAction);
            ToAccountSettingsPageCommand = new RelayCommand(ToAccountSettingsPageCommandAction);
            ToUserAddressesPageCommand = new RelayCommand(ToUserAddressesPageCommandAction);
            ToUserWishlistsPageCommand = new RelayCommand(ToUserWishlistsPageCommandAction);
        }

        public async Task<IEnumerable<Client>> GetAllClientsAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Client>>($"{BaseUrl}/all");
        }

        public async Task<Client> GetClientByIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<Client>($"{BaseUrl}/{id}");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Failed to get client by ID: {ex.Message}", "OK");
                return null;
            }
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

        public async Task<Client> LoginAsync(string email, string password)
        {
            var client = new Client { Email = email, Password = password, Name = "null", Surname = "null" };
            var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}/login", client);
            if (!response.IsSuccessStatusCode)
            {
                await Application.Current.MainPage.DisplayAlert("Login failed", "Invalid username or password.", "OK");
                return null;
            }
            else 
            {
                return await response.Content.ReadFromJsonAsync<Client>();
            }
        }

        public async void LoginCommandAction()
        {
            var client = await LoginAsync(email, password);
            if (client != null)
            {
                await SecureStorage.SetAsync("ClientId", client.Id.ToString());
                await SecureStorage.SetAsync("IsLoggedIn", "true");
                await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
            }
        }


        public async void ToRegisterPageCommandAction()
        {
            await Shell.Current.GoToAsync($"{nameof(RegistrationPage)}");
        }

        public async void SignUpCommandAction()
        {
            var client = new Client
            {
                Name = Name,
                Surname = Surname,
                Email = Email,
                Password = Password,
            };
            var response = await AddClientAsync(client);
            if (response)
            {
                await Shell.Current.GoToAsync($"..");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Invalid username or password.", "OK");
            }
        }

        public async void ToLoginPageCommandAction()
        {
            await Shell.Current.GoToAsync($"{nameof(LoginPage)}");
        }

        public async void ToUserOrderHistoryPageCommandAction()
        {
            await Shell.Current.GoToAsync($"{nameof(UserOrderHistoryPage)}");
        }

        public async void ToAccountSettingsPageCommandAction()
        {
            await Shell.Current.GoToAsync($"{nameof(AccountSettingsPage)}");
        }

        public async void ToUserAddressesPageCommandAction()
        {
            await Shell.Current.GoToAsync($"{nameof(UserAddressesPage)}");
        }

        public async void ToUserWishlistsPageCommandAction()
        {
            await Shell.Current.GoToAsync($"{nameof(UserWishlistsPage)}");
        }

        public async void LogOutCommandAction()
        {
            if (await Application.Current.MainPage.DisplayAlert("Log out", "Are you sure you want to log out?", "Yes", "No"))
            {
                SecureStorage.RemoveAll();
                ClearClientData();
                var stack = Shell.Current.Navigation.NavigationStack.ToArray();
                for (int i = stack.Length - 1; i > 0; i--)
                {
                    Shell.Current.Navigation.RemovePage(stack[i]);
                }
                await Shell.Current.GoToAsync($"{nameof(LoginPage)}");
            }
        }

        private void ClearClientData()
        {
            Id = 0;
            Name = string.Empty;
            Surname = string.Empty;
            Email = string.Empty;
            Password = string.Empty;
            Role = string.Empty;
            Phone = string.Empty;
            Addresses?.Clear();
        }
        public async Task LoadClientDataAsync()
        {
            try
            {
                if (await IsUserLoggedIn())
                {
                    var clientId = await SecureStorage.GetAsync("ClientId");
                    if (clientId != null)
                    {
                        var client = await GetClientByIdAsync(int.Parse(clientId));
                        Id = client.Id;
                        Name = client.Name;
                        Surname = client.Surname;
                        Email = client.Email;
                        Password = client.Password;
                        Role = client.Role;
                        Phone = client.Phone;
                        Addresses = client.Addresses.ToList() ?? new List<Address>();
                        return;
                    }
                }
                else
                {
                    await Shell.Current.GoToAsync(nameof(LoginPage));
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Failed to load client data: {ex.Message}", "OK");
            }
        }
        private async Task<bool> IsUserLoggedIn()
        {
            var isLogged = await SecureStorage.GetAsync("IsLoggedIn");
            return isLogged != null && isLogged == "true";
        }
    }
}
