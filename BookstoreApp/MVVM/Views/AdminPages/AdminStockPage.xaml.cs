namespace BookstoreApp.MVVM.Views.AdminPages;

public partial class AdminStockPage : ContentPage
{
	public AdminStockPage()
	{
		InitializeComponent();
	}

    private async void AddNewBookButtonClicked(object sender, EventArgs e)
    {
		await Shell.Current.GoToAsync($"{nameof(AddBookPage)}");
    }
}