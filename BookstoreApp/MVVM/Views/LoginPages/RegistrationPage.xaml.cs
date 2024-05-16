namespace BookstoreApp.MVVM.Views.LoginPages;

public partial class RegistrationPage : ContentPage
{
	public RegistrationPage()
	{
		InitializeComponent();
	}

    private async void RegisterButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"..");
    }
}