using Bookstore_MAUI.MVVM.ViewModels;

namespace BookstoreApp.MVVM.Views.LoginPages;

public partial class RegistrationPage : ContentPage
{
    private readonly ClientViewModel _clientVM;
	public RegistrationPage(ClientViewModel clientViewModel)
	{
		InitializeComponent();
        _clientVM = clientViewModel;
        BindingContext = _clientVM;
    }
}