using Bookstore_MAUI.MVVM.ViewModels;
using BookstoreClassLibrary.Models;

namespace BookstoreApp.MVVM.Views.ApplicationPages;

public partial class OrderPage : ContentPage
{
    private readonly OrderViewModel _orderVM;
    private readonly ClientViewModel _clientVM;
    public OrderPage(OrderViewModel orderVM, ClientViewModel clientVM)
    {
        InitializeComponent();
        BindingContext = _orderVM = orderVM;
        _clientVM = clientVM;
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
        }
    }

    private async Task<bool> IsUserLoggedIn()
    {
        var isLogged = await SecureStorage.GetAsync("IsLoggedIn");
        return isLogged != null && isLogged == "true";
    }
}