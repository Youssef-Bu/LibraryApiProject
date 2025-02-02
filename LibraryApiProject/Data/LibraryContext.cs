using LibraryApiProject.Models;
using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;

public class LibraryContext : DbContext
{
    public LibraryContext(DbContextOptions<LibraryContext> options) : base(options) { }

    public DbSet<Book> Books { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Publisher> Publishers { get; set; }
    public DbSet<Loan> Loans { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>().HasIndex(b => b.ISBN).IsUnique();
        modelBuilder.Entity<Category>().HasIndex(c => c.Name).IsUnique();
        modelBuilder.Entity<Publisher>().HasIndex(p => p.Name).IsUnique();
        modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();

        modelBuilder.Entity<Book>()
            .HasOne(b => b.Category)
            .WithMany(c => c.Books)
            .HasForeignKey(b => b.CategoryId);
        modelBuilder.Entity<Book>()
            .HasOne(b => b.Publisher)
            .WithMany(p => p.Books)
            .HasForeignKey(b => b.PublisherId);
    }
}

