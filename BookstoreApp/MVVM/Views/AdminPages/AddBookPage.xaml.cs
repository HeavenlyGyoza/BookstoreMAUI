using Bookstore_MAUI.MVVM.ViewModels;
using BookstoreClassLibrary.Models;

namespace BookstoreApp.MVVM.Views.AdminPages;

public partial class AddBookPage : ContentPage
{
	private readonly BookViewModel _bookVM;
	private MemoryStream _bookCoverStream;
	public AddBookPage(BookViewModel bookViewModel)
	{
		InitializeComponent();
		BindingContext = _bookVM = bookViewModel;
	}

	private async void AddBookButtonClicked(object sender, EventArgs e)
	{
		var book = new Book
		{
			Title = titleEntry.Text,
			Isbn = isbnEntry.Text,
			Publisher = publisherEntry.Text,
			Genre = genreEntry.Text,
			PublishDate = DateOnly.FromDateTime(pubDateDatePicker.Date),
			Language = languageEntry.Text,
			PageSize = int.Parse(pageSizeEntry.Text),
			Description = descriptionEditor.Text,
			Stock = int.Parse(stockEntry.Text),
			Price = decimal.Parse(priceEntry.Text),
			Authors = authorsEntry.Text.Split(',').Select(name => new Author { Name = name.Trim() }).ToList()
		};

		bool success = await _bookVM.AddBookAsync(book, _bookCoverStream);
		if (success)
		{
			await DisplayAlert("Sucess", "Book added succesfully!", "OK");
			await Shell.Current.GoToAsync("..");
		}
		else
		{
			await DisplayAlert("Error", "Failed to add book.", "OK");
		}
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
			_bookCoverStream = new MemoryStream();
			await stream.CopyToAsync(_bookCoverStream);
			_bookCoverStream.Position = 0;
		}
    }
}