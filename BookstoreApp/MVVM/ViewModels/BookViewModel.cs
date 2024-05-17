using BookstoreClassLibrary.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore_MAUI.MVVM.ViewModels
{
    public partial class BookViewModel : ObservableObject
    {
        private readonly HttpClient _httpClient;
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
        public string coverImage;

        public BookViewModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Book>>($"{BaseUrl}/all");
        }

        public async Task<Book> GetBookByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Book>($"{BaseUrl}/{id}");
        }

        public async Task<bool> AddBookAsync(Book book)
        {
            var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}/add", book);
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
    }
}
