﻿using Microsoft.EntityFrameworkCore;
using BooksAI.Models;

namespace BooksAI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) 
        {
            Database.EnsureCreated();
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
    }
}
