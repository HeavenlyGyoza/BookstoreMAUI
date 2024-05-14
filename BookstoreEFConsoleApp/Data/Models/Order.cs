using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreEFConsoleApp.Data.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey(nameof(Client.Id))]
        public int ClientId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public DateOnly OrderDate { get; set; }
        [Required]
        public Client Client { get; set; }
        [Required]
        public Book Book { get; set; }
        [Required]
        public Address Address { get; set; }
    }
}
