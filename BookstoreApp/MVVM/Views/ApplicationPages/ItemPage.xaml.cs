using Bookstore_MAUI.MVVM.ViewModels;

namespace BookstoreApp.MVVM.Views.ApplicationPages;

public partial class ItemPage : ContentPage
{
    private readonly BookViewModel _bookVM;
    public ItemPage(BookViewModel bookViewModel)
	{
		InitializeComponent();
		BindingContext = _bookVM = bookViewModel;
    }
}