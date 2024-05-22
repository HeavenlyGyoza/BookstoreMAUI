using Bookstore_MAUI.MVVM.ViewModels;
using BookstoreApp.MVVM.Views.ApplicationPages;

namespace BookstoreApp.MVVM.Views.NavigationPages;

public partial class HomePage : ContentPage
{
	private readonly BookViewModel _bookVM;
	public HomePage(BookViewModel bookViewModel)
	{
		InitializeComponent();
		BindingContext = _bookVM = bookViewModel;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (_bookVM.LoadBooksCommand.CanExecute(null))
        {
            _bookVM.LoadBooksCommand.Execute(null);
        }
    }

    private async void SearchItems(object sender, EventArgs e)
    {
		string query = ((SearchBar)sender).Text;
		await Shell.Current.GoToAsync($"{nameof(ExplorePage)}?query={Uri.EscapeDataString(query)}");
    }
}