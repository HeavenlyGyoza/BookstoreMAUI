using BookstoreClassLibrary.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore_MAUI.MVVM.ViewModels
{
    public partial class AuthorViewModel : ObservableObject
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://localhost:7299/Author";

        public int Id { get; set; }
        [ObservableProperty]
        public string name;
        [ObservableProperty]
        public List<Book> books;

        public ObservableCollection<Author> Authors { get; set; }

        public AuthorViewModel (HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Author>> GetAllAuthorsAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Author>>($"{BaseUrl}/all");
        }

        public async Task<Author> GetAuthorByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Author>($"{BaseUrl}/{id}");
        }

        public async Task<bool> AddAuthorAsync(Author author)
        {
            var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}/add", author);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAuthorAsync(Author author)
        {
            if (author == null)
            {
                return false;
            }
            var response = await _httpClient.PutAsJsonAsync($"{BaseUrl}/{author.Id}", author);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAuthorAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/{id}");
            return response.IsSuccessStatusCode;
        }

        //TODO make API first
        //public async Task SearchBooksByAuthorAsync(string query)
        //{
        //    var response = await _httpClient.GetFromJsonAsync<Author>($"{BaseUrl}?query={query}");
        //    if (response.IsSuccessStatusCode)
        //    {
        //        var 
        //    }
        //}

        //public async Task<IEnumerable<Author>> GetAuthorByNameAsync(string query)
        //{
        //    Authors.Clear();
        //    var response = await _httpClient.GetFromJsonAsync<IEnumerable<Author>>($"{BaseUrl}?query={query}");
        //    foreach (var author in response)
        //    {
        //        Authors.Add(author);
        //    }
        //    return Authors;
        //}
    }
}
