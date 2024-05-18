using BookstoreClassLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.MVVM.Services
{
    public class SearchService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://localhost:7299";

        public SearchService (HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<Author>> SearchAuthorsAsync(string query)
        {
            var authors = await _httpClient.GetFromJsonAsync<IEnumerable<Author>>($"{BaseUrl}/Author/byName?query={query}");

            return authors;
        }

        public async Task<IEnumerable<Book>> SearchBooksAsync(string query)
        {
            var books = new List<Book>();

            var authors = await SearchAuthorsAsync(query);
            if (authors != null)
            {
                foreach (var author in authors)
                {
                    books.AddRange(author.Books);
                }
            }

            var booksByTitle = await _httpClient.GetFromJsonAsync<IEnumerable<Book>>($"{BaseUrl}/Book/byTitle?query={query}");
            if (booksByTitle != null)
            {
                books.AddRange(booksByTitle.Where(b => !books.Contains(b)));
            }
            return books;
        }
    }
}