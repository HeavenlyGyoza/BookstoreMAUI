using CommunityToolkit.Mvvm.ComponentModel;
using BookstoreWebAPI.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.MVVM.ViewModels
{
    public partial class ClientViewModel : ObservableObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public List<Address> Addresses { get; set; }
    }
}
