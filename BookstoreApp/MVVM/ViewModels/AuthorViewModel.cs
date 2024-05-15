using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
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
        public string Name { get; set; }
        public List<BookViewModel> Books { get; set; }

        public AuthorViewModel (HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<AuthorViewModel>> GetAllAuthorsAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<AuthorViewModel>>($"{BaseUrl}/all");
        }

        public async Task<AuthorViewModel> GetAuthorByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<AuthorViewModel>($"{BaseUrl}/{id}");
        }

        public async Task<bool>AddAuthorAsync(AuthorViewModel author)
        {
            var response = await _httpClient.PostAsJsonAsync($"{BaseUrl}/add", author);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAuthorAsync(AuthorViewModel author)
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
    }
}
