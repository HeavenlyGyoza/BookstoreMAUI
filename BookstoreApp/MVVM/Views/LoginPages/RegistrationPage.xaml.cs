using Bookstore_MAUI.MVVM.ViewModels;

namespace BookstoreApp.MVVM.Views.LoginPages;

public partial class RegistrationPage : ContentPage
{
    private readonly UserViewModel _userVM;
	public RegistrationPage(UserViewModel userViewModel)
	{
		InitializeComponent();
        BindingContext = _userVM = userViewModel;
	}
}