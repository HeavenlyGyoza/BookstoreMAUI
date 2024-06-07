using Bookstore_MAUI.MVVM.ViewModels;
using BookstoreApp.MVVM.Services.Localization;
using BookstoreApp.Resources.Languages;
using System.Globalization;

namespace BookstoreApp.MVVM.Views.NavigationPages;

public partial class UserPage : ContentPage
{
    private readonly ClientViewModel _clientVM;
    private readonly OrderViewModel _orderVM;
    public LocalizationResourceManager LocalizationResourceManager => LocalizationResourceManager.Instance;
    public UserPage(ClientViewModel clientViewModel, OrderViewModel orderVM)
	{
		InitializeComponent();
        BindingContext = _clientVM = clientViewModel;
        _orderVM = orderVM;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (_clientVM != null)
        {
            signInButton.IsVisible = false;
            userGrid.IsVisible = true;
        }
        if (_orderVM != null && _orderVM.LoadClientOrdersCommand.CanExecute(null))
        {
            BindingContext = _orderVM; //necessary to show orders collection
            _orderVM.LoadClientOrdersCommand.Execute(null);
        }
    }

    private void rbEnglish_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (e.Value)
        {
            var switchToCulture = new CultureInfo("en-US");
            BindingContext = this;
            LocalizationResourceManager.SetCulture(switchToCulture);
        }

    }

    private void rbSpanish_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (e.Value)
        {
            var switchToCulture = new CultureInfo("es-ES");
            BindingContext = this;
            LocalizationResourceManager.SetCulture(switchToCulture);
        }
    }
}