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
		if (searchBarCell.IsVisible)
		{
			await searchBarCell.TranslateTo(0, searchBarCell.Height, 250);
            //await searchBarCell.FadeTo(0, 250);
            searchBarCell.IsVisible = false;
		}
		else
		{
            searchBarCell.IsVisible = true;
			await searchBarCell.TranslateTo(0, 0, 250);
            searchBarCell.Focus();
            //await searchBarCell.FadeTo(1, 250);
        }
    }

    private async void SearchItems(object sender, EventArgs e)
    {
		string query = ((SearchBar)sender).Text;
		await Shell.Current.GoToAsync($"{nameof(ExplorePage)}?query={Uri.EscapeDataString(query)}");
    }
}