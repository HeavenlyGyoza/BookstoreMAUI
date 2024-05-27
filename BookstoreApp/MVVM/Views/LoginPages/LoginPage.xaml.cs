using Bookstore_MAUI.MVVM.ViewModels;
using BookstoreApp.MVVM.Views.NavigationPages;

namespace BookstoreApp.MVVM.Views.LoginPages;

public partial class LoginPage : ContentPage
{
    private readonly ClientViewModel _clientVM;
	public LoginPage(ClientViewModel clientViewModel)
	{
		InitializeComponent();
        BindingContext = _clientVM = clientViewModel;
	}

    private async void LoginButtonClicked(object sender, EventArgs e)
    {
        var client = await _clientVM.LoginAsync(_clientVM.Email, _clientVM.Password);
        if (client != null)
        {
            await SecureStorage.SetAsync("IsLoggedIn", "true");
            await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
        }
    }

    private async void NoLoginTapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
    }
}