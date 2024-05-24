﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreClassLibrary.Models
{
    public class Wishlist
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey(nameof(User.Id))]
        public int UserId { get; set; }
        public User User { get; set; }
        public string WishlistName { get; set; }
        public ICollection<Book>? Books { get; set;}
    }
}