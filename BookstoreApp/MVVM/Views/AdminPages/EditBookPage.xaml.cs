using Bookstore_MAUI.MVVM.ViewModels;
using BookstoreClassLibrary.Models;

namespace BookstoreApp.MVVM.Views.AdminPages;

public partial class EditBookPage : ContentPage
{
    private readonly BookViewModel _bookVM;

    public EditBookPage(BookViewModel bookViewModel)
	{
		InitializeComponent();
		BindingContext = _bookVM = bookViewModel;

	}

    private async void FilePickerButtonClicked(object sender, EventArgs e)
    {
        var result = await FilePicker.PickAsync(new PickOptions
        {
            FileTypes = FilePickerFileType.Images,
            PickerTitle = "Please select an image"
        });

        if (result != null)
        {
            bookCoverImage.Source = ImageSource.FromFile(result.FullPath);
            var stream = await result.OpenReadAsync();
            _bookVM.coverImageStream = new MemoryStream();
            await stream.CopyToAsync(_bookVM.coverImageStream);
            _bookVM.coverImageStream.Position = 0;
        }
    }
}