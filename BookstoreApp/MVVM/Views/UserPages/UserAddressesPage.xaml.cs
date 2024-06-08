using Bookstore_MAUI.MVVM.ViewModels;
using BookstoreClassLibrary.Models;

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

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _addressVM.LoadClientAddressesCollection(_clientVM.Id);

    }

    private async void RadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (e.Value)
        {
            var selectedAddress = (Address)((RadioButton)sender).BindingContext;
            foreach (var address in _addressVM.ClientAddresses)
            {
                if (address != selectedAddress && address.IsPrimary)
                {
                    address.IsPrimary = false;
                    await _addressVM.UpdateAddressAsync(address);
                }
            }
            selectedAddress.IsPrimary = true;
            await _addressVM.UpdateAddressAsync(selectedAddress);
        }
    }
}