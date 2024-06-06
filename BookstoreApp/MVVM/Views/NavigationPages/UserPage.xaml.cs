using Bookstore_MAUI.MVVM.ViewModels;

namespace BookstoreApp.MVVM.Views.NavigationPages;

public partial class UserPage : ContentPage
{
    private readonly ClientViewModel _clientVM;
    private readonly OrderViewModel _orderVM;
    public UserPage(ClientViewModel clientViewModel, OrderViewModel orderVM)
	{
		InitializeComponent();
        BindingContext = _clientVM = clientViewModel;
        _orderVM = orderVM;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (_clientVM != null)
        {
            signInButton.IsVisible = false;
            userGrid.IsVisible = true;
        }
        if (_orderVM != null && _orderVM.LoadClientOrdersCommand.CanExecute(null))
        {
            BindingContext = _orderVM; //necessary to show orders collection
            _orderVM.LoadClientOrdersCommand.Execute(null);
        }
    }

}