using BookstoreApp.MVVM.Views.AdminPages;

namespace BookstoreApp.MVVM.Views.NavigationPages;

public partial class ManagementPage : ContentPage
{
	public ManagementPage()
	{
		InitializeComponent();
	}

    private async void ManageStockButtonClicked(object sender, EventArgs e)
    {
		await Shell.Current.GoToAsync($"{(nameof(AdminStockPage))}");
    }
}