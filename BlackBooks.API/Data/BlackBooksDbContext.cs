using BlackBooks.API.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlackBooks.API.Data;

public class BlackBooksDbContext : DbContext
{
    public DbSet<Book> Books => this.Set<Book>();

    public BlackBooksDbContext(DbContextOptions<BlackBooksDbContext> options) 
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>().HasData(
            new Book { Id = 1, Title = "Book 1 - The Amazing Book" },
            new Book { Id = 2, Title = "Book 2 - The Terrible Book" }
            );
        base.OnModelCreating(modelBuilder);
    }
}
