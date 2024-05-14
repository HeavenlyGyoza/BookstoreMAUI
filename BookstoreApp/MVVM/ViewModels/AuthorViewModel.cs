using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore_MAUI.MVVM.ViewModels
{
    public partial class AuthorViewModel : ObservableObject
    {
        //private readonly ApplicationDbContext _dbContext;

        public int Id { get; set; }
        public string Name { get; set; }
        public List<BookViewModel> Books { get; set; }


    }
}
