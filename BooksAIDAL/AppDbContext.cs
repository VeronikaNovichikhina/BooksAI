
using BooksAIDAL.Models;
using BooksAIDomain.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BooksAIDAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) 
        {
            Database.EnsureCreated();
        }
        public DbSet<BooksEntity> Books { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Role> Roles { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new Role { Id = 1, Name = "admin" },
                new Role { Id = 2, Name = "user" }
            );
            base.OnModelCreating(modelBuilder);
        }
    }
}
