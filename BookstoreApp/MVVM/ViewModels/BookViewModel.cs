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
        public string Isbn { get; set; }
        public string Title { get; set; }
        public List<AuthorViewModel> Authors { get; set; }
        public string Publisher { get; set; }
        public string Genre { get; set; }
        public DateOnly PubDate {  get; set; }
        public string Language { get; set; }
        public int PageCount { get; set; }
        public int Stock {  get; set; }
        public decimal Price { get; set; }
        public float Discount { get; set; }
        public string Description { get; set; }

        public BookViewModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<BookViewModel>> GetAllBooksAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<BookViewModel>>($"{BaseUrl}/all");
        }

        public async Task<BookViewModel> GetBookByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<BookViewModel>($"{BaseUrl}/{id}");
        }

        public async Task<bool> AddBookAsync(BookViewModel book)
        {
            var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}/add", book);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateBookAsync(BookViewModel book)
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
