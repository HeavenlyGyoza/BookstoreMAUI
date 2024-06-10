using Bookstore_MAUI.MVVM.ViewModels;

namespace BookstoreApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NBaF1cXmhPYVJ/WmFZfVpgdVdMYl5bRXRPIiBoS35RckVmW39fcnZXR2NYWUB3");
            Application.Current.UserAppTheme = AppTheme.Light;
        }

        //private void CheckUserRole()
        //{
        //    if (_clientVM.Role != "admin")
        //    {
        //        managementTab.IsVisible = false;
        //    }
        //}
    }
}
