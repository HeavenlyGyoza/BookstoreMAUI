
using BookstoreApp.MVVM.Views.LoginPages;

namespace BookstoreApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }

        protected override async void OnStart()
        {
            base.OnStart();
            if (!await IsUserLoggedIn())
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
