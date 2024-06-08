using Bookstore_MAUI.MVVM.ViewModels;
using BookstoreApp.MVVM.Services.Localization;
using System.Globalization;

namespace BookstoreApp.MVVM.Views.UserPages;

public partial class AccountSettingsPage : ContentPage
{
    private readonly ClientViewModel _clientVM;
    private bool _IsInitialized = false;
    public AccountSettingsPage(ClientViewModel clientVM)
	{
		InitializeComponent();
		BindingContext = _clientVM = clientVM;
        _IsInitialized = true;
        SetLanguageRG();
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
        else if (_clientVM.lang.Name == "es-ES")
        {
            rbSpanish.IsChecked = true;
        }
    }
}