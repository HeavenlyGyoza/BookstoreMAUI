
using Bookstore_MAUI.MVVM.ViewModels;
using BookstoreApp.MVVM.Services.Localization;
using BookstoreApp.MVVM.Views.LoginPages;
using System.Globalization;

namespace BookstoreApp
{
    public partial class App : Application
    {
        private readonly ClientViewModel _clientViewModel;
        public App(ClientViewModel clientViewModel)
        {
            InitializeComponent();
            _clientViewModel = clientViewModel;
            var savedCulture = SecureStorage.GetAsync("AppLanguage").GetAwaiter().GetResult();
            var culture = savedCulture != null ? new CultureInfo(savedCulture) : new CultureInfo("en-US");
            LocalizationResourceManager.Instance.SetCulture(culture);
            _clientViewModel.lang = culture;
            MainPage = new AppShell(_clientViewModel);
        }

        protected override async void OnStart()
        {
            base.OnStart();
            try
            {
                await _clientViewModel.LoadClientDataAsync();
            }
            catch (Exception ex)
            {
                await Current.MainPage.DisplayAlert("Error", $"Failed to load client data: {ex.Message}", "OK");
                await Shell.Current.GoToAsync(nameof(LoginPage));
            }
        }
    }
}
