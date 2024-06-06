using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreClassLibrary.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        [Precision(18, 2)]
        public decimal Price { get; set; }
        [Required]
        public DateOnly OrderDate { get; set; }
        [ForeignKey(nameof(Client))]
        [Required]
        public int ClientId { get; set; }
        public Client Client { get; set; }
        [ForeignKey(nameof(Book))]
        [Required]
        public int BookId { get; set; }
        public Book Book { get; set; }
        [ForeignKey(nameof(Address))]
        [Required]
        public int AddressId { get; set; }
        public Address Address { get; set; }
    }
}
