using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreClassLibrary.Models
{
    public class Client
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(30)]
        public string Name { get; set; }
        [Required]
        [StringLength(30)]
        public string Surname { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string? Phone { get; set; }
        [Required]
        [StringLength(64)]
        [MinLength(4)]
        public string Password { get; set; }
        public string Role { get; set; } = "User";
        public ICollection<Address>? Addresses { get; set; } = [];
        public ICollection<Order>? Orders { get; set; } = [];
        public ICollection<Wishlist>? Wishlists { get; set; } = [];
        //public ShoppingCart? ShoppingCart { get; set; }
    }
}
