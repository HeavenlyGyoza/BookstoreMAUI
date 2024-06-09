using Bookstore_MAUI.MVVM.ViewModels;
using BookstoreClassLibrary.Models;

namespace BookstoreApp.MVVM.Views.UserPages;

public partial class ManageUserAddressPage : ContentPage
{
    private readonly AddressViewModel _addressVM;
	private readonly ClientViewModel _clientVM;
    public ManageUserAddressPage(AddressViewModel addressVM, ClientViewModel clientVM)
    {
        InitializeComponent();
        BindingContext = _addressVM = addressVM;
        _clientVM = clientVM;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        Client client = new Client
        {
            Id = _clientVM.Id,
            Name = _clientVM.Name,
            Surname = _clientVM.Surname,
            Email = _clientVM.Email,
            Password = _clientVM.Password,
            Phone = _clientVM.Phone,
            Addresses = _clientVM.Addresses
        };
        _addressVM.client = client;
    }
}