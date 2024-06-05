using BookstoreClassLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookstoreWebAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public ApplicationDbContext() { }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Order> Orders { get; set; }
        //public DbSet<User> Users { get; set; }
        //public DbSet<Guest> Guests { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }
        //public DbSet<ShoppingCart> ShoppingCarts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Author>()
                        .HasMany(a => a.Books)
                        .WithMany(b => b.Authors);

            modelBuilder.Entity<Client>()
                        .HasMany(c => c.Addresses)
                        .WithMany(d => d.Clients);

            modelBuilder.Entity<Client>()
                        .HasMany(c => c.Orders)
                        .WithOne(o => o.Client);

            modelBuilder.Entity<Client>()
                        .HasMany(c => c.Wishlists)
                        .WithOne(w => w.Client);

        }
    }
}
