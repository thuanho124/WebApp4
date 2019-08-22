using LibraryManagementWithAuthen.Models;
using Microsoft.EntityFrameworkCore;
using LibraryManagementWithAuthen.Data;


// this context for book, student, etc
namespace LibraryManagementWithAuthen.Data
{
    public class LibDbContext : DbContext
    {
        public LibDbContext(DbContextOptions<LibDbContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<RentedBook> RentedBooks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().ToTable("Book");
            modelBuilder.Entity<Student>().ToTable("Student");
            modelBuilder.Entity<RentedBook>().ToTable("RentedBook");
        }
    }
}