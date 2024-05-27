using Bookstore_MAUI.MVVM.ViewModels;

namespace BookstoreApp.MVVM.Views.NavigationPages;

public partial class UserPage : ContentPage
{
    private readonly ClientViewModel _clientVM;
    public UserPage(ClientViewModel clientViewModel)
	{
		InitializeComponent();
        BindingContext = _clientVM = clientViewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (_clientVM != null)
        {
            signInButton.IsVisible = false;
            userGrid.IsVisible = true;
        }
    }

}