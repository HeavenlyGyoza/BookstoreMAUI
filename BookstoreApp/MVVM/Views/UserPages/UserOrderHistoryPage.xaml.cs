using Bookstore_MAUI.MVVM.ViewModels;

namespace BookstoreApp.MVVM.Views.UserPages;

public partial class UserOrderHistoryPage : ContentPage
{
    private readonly OrderViewModel _orderVM;
    public UserOrderHistoryPage(OrderViewModel orderVM)
	{
		InitializeComponent();
		BindingContext = _orderVM = orderVM;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (_orderVM != null && _orderVM.LoadClientOrdersCommand.CanExecute(null))
        {
            _orderVM.LoadClientOrdersCommand.Execute(null);
        }
    }
}