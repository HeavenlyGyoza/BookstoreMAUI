using CommunityToolkit.Mvvm.ComponentModel;
using BookstoreWebAPI.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreApp.MVVM.ViewModels
{
    public partial class OrderViewModel : ObservableObject
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateOnly OrderDate { get; set; }
        public Client Client { get; set; }
        public Book Book { get; set; }
        public Address Address { get; set; }

    }
}
