using CommunityToolkit.Mvvm.ComponentModel;
using BookstoreWebAPI.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.MVVM.ViewModels
{
    public partial class AuthorViewModel : ObservableObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Book> Books { get; set; }
    }
}
