using CommunityToolkit.Mvvm.ComponentModel;
using BookstoreWebAPI.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.MVVM.ViewModels
{
    public partial class BookViewModel : ObservableObject
    {

        public int Id { get; set; }
        public string Isbn { get; set; }
        public string Title { get; set; }
        public List<Author> Authors { get; set; }
        public string Publisher { get; set; }
        public string Genre { get; set; }
        public DateOnly PubDate { get; set; }
        public string Language { get; set; }
        public int PageCount { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public float Discount { get; set; }
        public string Description { get; set; }
    }
}
