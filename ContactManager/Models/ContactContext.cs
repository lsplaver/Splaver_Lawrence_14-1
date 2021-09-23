using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.Models
{
    public class ContactContext : DbContext
    {
        public ContactContext(DbContextOptions<ContactContext> options) : base(options)
        { }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>().HasData(
                new Contact
                {
                    ContactId = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    Phone = "541-534-4923",
                    Email = "john.doe@yahoo.com",
                    CategoryId = 1,
                    Organization = ""
                },
                new Contact
                {
                    ContactId = 2,
                    FirstName = "Jane",
                    LastName = "Smith",
                    Phone = "316-421-4521",
                    Email = "jsmith@gmail.com",
                    CategoryId = 2,
                    Organization = ""
                }
            );

            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "Friend"},
                new Category { CategoryId = 2, Name = "Coworker"},
                new Category { CategoryId = 3, Name = "Family"},
                new Category { CategoryId = 4, Name = "Medical" },
                new Category { CategoryId = 5, Name = "Other"}
            );
        }
    }
}
