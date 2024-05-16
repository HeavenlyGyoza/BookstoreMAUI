using BookstoreApp.MVVM.Views.NavigationPages;

namespace BookstoreApp.MVVM.Views.LoginPages;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}

    protected override async void OnAppearing()
    {
        var LoggedIn = false;
        if (LoggedIn)
        {
            await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
        }
    }

    private async void LoginButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
    }

    private async void RegisterTapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync($"{nameof(RegistrationPage)}");
    }
}