using BookstoreApp.MVVM.Services;
using BookstoreApp.MVVM.Views.AdminPages;
using BookstoreClassLibrary.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Bookstore_MAUI.MVVM.ViewModels
{
    public partial class BookViewModel : ObservableObject
    {
        private readonly HttpClient _httpClient;
        private readonly SearchService _searchService;
        private const string BaseUrl = "https://localhost:7299/Book";

        public int Id { get; set; }
        [ObservableProperty]
        public string isbn;
        [ObservableProperty]
        public string title;
        [ObservableProperty]
        public List<Author> authors;
        [ObservableProperty]
        public string publisher;
        [ObservableProperty]
        public string genre;
        [ObservableProperty]
        public DateOnly pubDate;
        [ObservableProperty]
        public string language;
        [ObservableProperty]
        public int pageCount;
        [ObservableProperty]
        public int stock;
        [ObservableProperty]
        public decimal price;
        [ObservableProperty]
        public float discount;
        [ObservableProperty]
        public string description;
        [ObservableProperty]
        public string coverImagePath;
        [ObservableProperty]
        public Stream coverImageStream;

        public ObservableCollection<Book> Books { get; set; } = new ObservableCollection<Book>();

        public IRelayCommand LoadBooksCommand { get; }
        public IRelayCommand EditBookCommand { get; }
        public IRelayCommand DeleteBookCommand { get; }

        public BookViewModel() { }
        public BookViewModel(HttpClient httpClient, SearchService searchService)
        {
            _httpClient = httpClient;
            _searchService = searchService;
            LoadBooksCommand = new RelayCommand(async () => await LoadBooksCollection());
            EditBookCommand = new RelayCommand<Book>(EditBookCommandAction);
            DeleteBookCommand = new RelayCommand<Book>(DeleteBookCommandAction);
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Book>>($"{BaseUrl}/all");
        }

        public async Task<Book> GetBookByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Book>($"{BaseUrl}/{id}");
        }

        public async Task<bool> AddBookAsync(Book book, Stream coverImageStream)
        {

            var content = new MultipartFormDataContent();

            var bookJson = JsonSerializer.Serialize(book);
            content.Add(new StringContent(bookJson, Encoding.UTF8, "application/json"), "bookJson");

            var streamContent = new StreamContent(coverImageStream);
            streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg");
            content.Add(streamContent, "coverImage", "cover.jpeg");

            var response = await _httpClient.PostAsync($"{BaseUrl}/add", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateBookAsync(Book book)
        {
            var response = await _httpClient.PutAsJsonAsync($"{BaseUrl}/{book.Id}", book);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<Book>> SearchBooksByQuery(string query)
        {
            Books.Clear();
            var books = await _searchService.SearchBooksAsync(query);
            foreach (var book in books)
            {
                Books.Add(book);
            }
            return books;
        }

        private async Task LoadBooksCollection()
        {
            Books.Clear();
            var books = await GetAllBooksAsync();
            foreach (var book in books)
            {
                Books.Add(book);
            }
        }

        private async void EditBookCommandAction(Book book)
        {
            await Shell.Current.GoToAsync(nameof(EditBookPage), new Dictionary<string, object>
            {
                {"SelectedBook", book }
            });
        }

        private async void DeleteBookCommandAction(Book book)
        {
            var response = await DeleteBookAsync(book.Id);
            if (response)
            {
                Books.Remove(book);
            }
        }
    }
}
