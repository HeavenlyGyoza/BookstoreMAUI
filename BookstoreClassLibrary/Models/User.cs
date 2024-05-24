using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreClassLibrary.Models
{
    public class User : Client
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(64)]
        [MinLength(4)]
        public string Password { get; set; }
        public string Role { get; } = "User";
        public ICollection<Wishlist>? Wishlists { get; set; }
    }
}
