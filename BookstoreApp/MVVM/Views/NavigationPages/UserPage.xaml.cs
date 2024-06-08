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

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (_clientVM.Id > 0)
        {
            signInButton.IsVisible = false;
            userGrid.IsVisible = true;
        }
        else
        {
            signInButton.IsVisible = true;
            userGrid.IsVisible = false;
        }
    }
}