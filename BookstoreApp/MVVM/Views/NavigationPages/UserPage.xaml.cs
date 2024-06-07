using Bookstore_MAUI.MVVM.ViewModels;
using BookstoreApp.MVVM.Services.Localization;
using BookstoreApp.Resources.Languages;
using System.Globalization;

namespace BookstoreApp.MVVM.Views.NavigationPages;

public partial class UserPage : ContentPage
{
    private readonly ClientViewModel _clientVM;
    private bool _IsInitialized = false;
    public UserPage(ClientViewModel clientViewModel)
	{
		InitializeComponent();
        BindingContext = _clientVM = clientViewModel;
        _IsInitialized = true;
        SetLanguageRG();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (_clientVM.Id > 0)
        {
            signInButton.IsVisible = false;
            userGrid.IsVisible = true;
        }
    }

    private void rbEnglish_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (_IsInitialized && e.Value)
        {
            var switchToCulture = new CultureInfo("en-US");
            LocalizationResourceManager.Instance.SetCulture(switchToCulture);
            SecureStorage.SetAsync("AppLanguage", switchToCulture.Name);
        }

    }

    private void rbSpanish_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (_IsInitialized && e.Value)
        {
            var switchToCulture = new CultureInfo("es-ES");
            LocalizationResourceManager.Instance.SetCulture(switchToCulture);
            SecureStorage.SetAsync("AppLanguage", switchToCulture.Name);
        }
    }

    private void SetLanguageRG()
    {
        if (_clientVM.lang.Name == "en-US")
        {
            rbEnglish.IsChecked = true;
        }
        else if(_clientVM.lang.Name == "es-ES")
        {
            rbSpanish.IsChecked = true;
        }
    }
}