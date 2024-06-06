using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreClassLibrary.Models
{
    public class Address
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Street { get; set; }
        public string? AddInfo { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        [MaxLength(5)]
        public string PostalCode { get; set; }
        [Required]
        public string Province { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public bool IsPrimary { get; set; }
        public ICollection<Client> Clients { get; set; } = [];
    }
}
