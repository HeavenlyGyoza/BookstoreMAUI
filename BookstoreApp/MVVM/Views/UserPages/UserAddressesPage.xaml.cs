using Bookstore_MAUI.MVVM.ViewModels;

namespace BookstoreApp.MVVM.Views.UserPages;

public partial class UserAddressesPage : ContentPage
{
    private readonly ClientViewModel _clientVM;
	private readonly AddressViewModel _addressVM;
    public UserAddressesPage(ClientViewModel clientVM, AddressViewModel addressVM)
	{
		InitializeComponent();
		BindingContext = _addressVM = addressVM;
		_clientVM = clientVM;
	}
}