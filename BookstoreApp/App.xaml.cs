
using Bookstore_MAUI.MVVM.ViewModels;
using BookstoreApp.MVVM.Services.Localization;
using BookstoreApp.MVVM.Views.LoginPages;
using System.Globalization;

namespace BookstoreApp
{
    public partial class App : Application
    {
        private readonly ClientViewModel _clientViewModel;
        public LocalizationResourceManager LocalizationResourceManager => LocalizationResourceManager.Instance;
        public App(ClientViewModel clientViewModel)
        {
            InitializeComponent();
            _clientViewModel = clientViewModel;
            var switchToCulture = new CultureInfo("en-US");
            LocalizationResourceManager.SetCulture(switchToCulture);
            MainPage = new AppShell();
        }

        protected override async void OnStart()
        {
            base.OnStart();
            if (await IsUserLoggedIn())
            {
                var clientId = await SecureStorage.GetAsync("ClientId");
                if (clientId != null)
                {                    
                    var client = await _clientViewModel.GetClientByIdAsync(int.Parse(clientId));
                    _clientViewModel.Id = client.Id;
                    _clientViewModel.Name = client.Name;
                    _clientViewModel.Surname = client.Surname;
                    _clientViewModel.Email = client.Email;
                    _clientViewModel.Password = client.Password;
                    _clientViewModel.Role = client.Role;
                    _clientViewModel.Phone = client.Phone;
                    _clientViewModel.Addresses = client.Addresses.ToList();
                }
            }
            else
            {
                await Shell.Current.GoToAsync(nameof(LoginPage));
            }
        }

        private async Task<bool> IsUserLoggedIn()
        {
            var isLogged = await SecureStorage.GetAsync("IsLoggedIn");
            return isLogged != null && isLogged == "true";
        }
    }
}
