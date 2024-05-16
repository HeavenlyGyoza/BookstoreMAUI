using BookstoreApp.MVVM.Views.ApplicationPages;

namespace BookstoreApp.MVVM.Views.NavigationPages;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
	}

    private async void SearchButton_Clicked(object sender, EventArgs e)
    {
		if (SearchBar.IsVisible)
		{
			await SearchBar.TranslateTo(0, SearchBar.Height, 250);
			//await SearchBar.FadeTo(0, 250);
			SearchBar.IsVisible = false;
		}
		else
		{
			SearchBar.IsVisible = true;
			await SearchBar.TranslateTo(0, 0, 250);
			SearchBar.Focus();
			//await SearchBar.FadeTo(1, 250);
		}
    }

    private async void SearchItems(object sender, EventArgs e)
    {
		string query = ((SearchBar)sender).Text;
		await Shell.Current.GoToAsync($"{nameof(ExplorePage)}?query={Uri.EscapeDataString(query)}");
    }
}