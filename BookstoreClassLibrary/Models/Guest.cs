using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreClassLibrary.Models
{
    public class Guest : Client
    {
        public Order Order { get; set; }
        public Address Address { get; set; }
    }
}
