using Bookstore_MAUI.MVVM.ViewModels;

namespace BookstoreApp.MVVM.Views.UserPages;

public partial class ManageUserAddressPage : ContentPage
{
    private readonly AddressViewModel _addressVM;
    public ManageUserAddressPage(AddressViewModel addressVM)
	{
		InitializeComponent();
		BindingContext = _addressVM = addressVM;
	}
}