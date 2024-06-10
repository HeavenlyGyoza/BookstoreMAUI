using Bookstore_MAUI.MVVM.ViewModels;
using BookstoreClassLibrary.Models;

namespace BookstoreApp.MVVM.Views.ApplicationPages;

public partial class OrderPage : ContentPage
{
    private readonly OrderViewModel _orderVM;
    private readonly ClientViewModel _clientVM;
    private readonly AddressViewModel _addressVM;
    public OrderPage(OrderViewModel orderVM, ClientViewModel clientVM, AddressViewModel addressVM)
    {
        InitializeComponent();
        BindingContext = _orderVM = orderVM;
        _clientVM = clientVM;
        _addressVM = addressVM;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (await IsUserLoggedIn())
        {
            var clientId = await SecureStorage.GetAsync("ClientId");
            if (clientId != null)
            {
                _orderVM.Client = await _clientVM.GetClientByIdAsync(int.Parse(clientId));
            }
            //if (_orderVM.Client.Addresses.Any())
            //{
            //    _orderVM.Address = await _addressVM.GetPrimaryAddressByClientIdAsync(int.Parse(clientId));
            //}
        }
        _orderVM.Quantity = 1;
        _orderVM.Price = _orderVM.SelectedBook.Price;
        _orderVM.UpdateTotalPrice();
    }

    private async Task<bool> IsUserLoggedIn()
    {
        var isLogged = await SecureStorage.GetAsync("IsLoggedIn");
        return isLogged != null && isLogged == "true";
    }

    private void OnQuantityStepperChanged(object sender, ValueChangedEventArgs e)
    {
        _orderVM.Quantity = (int)e.NewValue;
        _orderVM.UpdateTotalPrice();
    }
}