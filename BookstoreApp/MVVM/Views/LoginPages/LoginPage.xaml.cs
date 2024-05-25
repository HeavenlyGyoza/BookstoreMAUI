using Bookstore_MAUI.MVVM.ViewModels;
using BookstoreApp.MVVM.Views.NavigationPages;

namespace BookstoreApp.MVVM.Views.LoginPages;

public partial class LoginPage : ContentPage
{
    private readonly UserViewModel _userVM;
	public LoginPage(UserViewModel userViewModel)
	{
		InitializeComponent();
        BindingContext = _userVM = userViewModel;
	}

    private async void LoginButtonClicked(object sender, EventArgs e)
    {
        var user = await _userVM.LoginAsync(_userVM.Email, _userVM.Password);
        if (user != null)
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