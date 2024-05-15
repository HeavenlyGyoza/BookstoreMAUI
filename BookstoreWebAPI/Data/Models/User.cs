using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreWebAPI.Data.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(128)]
        [MinLength(4)]
        public string Password { get; set; }
        public string Role { get; } = "User";
        public Client Client { get; set; }
    }
}
