using CommunityToolkit.Mvvm.ComponentModel;
using BookstoreWebAPI.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BookstoreApp.MVVM.ViewModels
{
    internal class UserViewModel : ObservableObject
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public Client Client { get; set; }
    }
}
