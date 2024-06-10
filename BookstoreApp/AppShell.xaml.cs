using Bookstore_MAUI.MVVM.ViewModels;

namespace BookstoreApp
{
    public partial class AppShell : Shell
    {
        private readonly ClientViewModel _clientVM;
        public AppShell(ClientViewModel clientVM)
        {
            InitializeComponent();
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NBaF1cXmhPYVJ/WmFZfVpgdVdMYl5bRXRPIiBoS35RckVmW39fcnZXR2NYWUB3");
            Application.Current.UserAppTheme = AppTheme.Light;
            _clientVM = clientVM;
            CheckUserRole();
        }

        private void CheckUserRole()
        {
            if (_clientVM.Role != "admin")
            {
                managementTab.IsVisible = false;
            }
        }

        //protected override void OnAppearing()
        //{
        //    base.OnAppearing();
        //    if (_clientVM.Role != "admin")
        //    {
        //        managementTab.IsVisible = false;
        //    }
        //}
    }
}
