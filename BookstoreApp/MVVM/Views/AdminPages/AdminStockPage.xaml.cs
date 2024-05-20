using Bookstore_MAUI.MVVM.ViewModels;

namespace BookstoreApp.MVVM.Views.AdminPages;
public partial class AdminStockPage : ContentPage
{
    private readonly BookViewModel _bookVM;
    public AdminStockPage(BookViewModel bookVM)
    {
        InitializeComponent();
        BindingContext = _bookVM = bookVM;
    }
    
    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (_bookVM.LoadBooksCommand.CanExecute(null))
        {
            _bookVM.LoadBooksCommand.Execute(null);
        }
    }

    private async void AddNewBookButtonClicked(object sender, EventArgs e)
    {
		await Shell.Current.GoToAsync($"{nameof(AddBookPage)}");
    }
}