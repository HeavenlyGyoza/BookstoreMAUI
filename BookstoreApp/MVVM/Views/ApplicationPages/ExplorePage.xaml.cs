using Bookstore_MAUI.MVVM.ViewModels;

namespace BookstoreApp.MVVM.Views.ApplicationPages;

[QueryProperty(nameof(query), "query")]
public partial class ExplorePage : ContentPage
{
    public string query { get; set; }
    private readonly BookViewModel _bookVM;
    public ExplorePage(BookViewModel bookViewModel)
	{
		InitializeComponent();
        BindingContext = _bookVM = bookViewModel;
	}

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        LoadBooks();
    }
    private async void LoadBooks()
    {
        await _bookVM.SearchBooksByQuery(query);
    }
}