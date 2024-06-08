using Bookstore_MAUI.MVVM.ViewModels;

namespace BookstoreApp.MVVM.Views.UserPages;

public partial class UserWishlistsPage : ContentPage
{
    private readonly ClientViewModel _clientVM;
    public UserWishlistsPage(ClientViewModel clientVM)
	{
		InitializeComponent();
		BindingContext = _clientVM = clientVM;
	}
}