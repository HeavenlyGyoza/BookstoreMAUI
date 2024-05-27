using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreClassLibrary.Models
{
    public class ShoppingCart
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey(nameof(Client.Id))]
        public int? ClientId { get; set; }
        public Client Client { get; set; }
        public ICollection<Book>? Books { get; set; }
    }
}
