using Bookstore_MAUI.MVVM.ViewModels;
using BookstoreClassLibrary.Models;

namespace BookstoreApp.MVVM.Views.AdminPages;

[QueryProperty(nameof(Id), nameof(Id))]
public partial class EditBookPage : ContentPage
{
    private readonly BookViewModel _bookVM;

    public EditBookPage(BookViewModel bookViewModel)
	{
		InitializeComponent();
		BindingContext = _bookVM = bookViewModel;
		//var bookId = id;

	}

    //protected override void OnNavigatedTo(NavigatedToEventArgs args)
    //{
    //    base.OnNavigatedTo(args);
    //}

    protected override void OnAppearing()
    {
        base.OnAppearing();
    }
}