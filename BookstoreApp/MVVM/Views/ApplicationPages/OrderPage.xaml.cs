using Bookstore_MAUI.MVVM.ViewModels;

namespace BookstoreApp.MVVM.Views.ApplicationPages;

public partial class OrderPage : ContentPage
{
    private readonly BookViewModel _bookVM;
    public OrderPage(BookViewModel bookVM)
    {
        InitializeComponent();
        BindingContext = _bookVM = bookVM;
    }
}