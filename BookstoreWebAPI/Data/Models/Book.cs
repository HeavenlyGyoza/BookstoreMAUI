using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreWebAPI.Data.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(17), MinLength(10)]
        public string Isbn { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public ICollection<Author> Authors { get; set; }
        [Required]
        public string Publisher { get; set; }
        [Required]
        public DateOnly PublishDate { get; set; }
        [Required]
        public int PageSize { get; set; }
        [Required]
        public string Language { get; set; }
        public string Genre { get; set; }
        [Required]
        public int Stock { get; set; }
        [Required]
        [Precision(18, 2)]
        public decimal Price { get; set; }
        public string Description { get; set; }
    }
}
