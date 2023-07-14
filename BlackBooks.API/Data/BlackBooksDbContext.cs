using BlackBooks.API.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlackBooks.API.Data;

public class BlackBooksDbContext : DbContext
{
    public DbSet<Book> Books => this.Set<Book>();
    public DbSet<Author> Authors => this.Set<Author>();

    public BlackBooksDbContext(DbContextOptions<BlackBooksDbContext> options) 
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>().HasData(
            new Author { Id = 1, Name = "Jules Verne" },
            new Author { Id = 2, Name = "H.G. Wells" },
            new Author { Id = 3, Name = "J.R.R. Tolkien" }
            );
        
        modelBuilder.Entity<Book>().HasData(
            new Book { Id = 1, Title = "Journey to the Center of the Earth", AuthorId = 1 },
            new Book { Id = 2, Title = "Twenty Thousand Leagues Under the Sea", AuthorId = 1 },
            new Book { Id = 3, Title = "The Time Machine", AuthorId = 2 },
            new Book { Id = 4, Title = "The War of the Worlds", AuthorId = 2 },
            new Book { Id = 5, Title = "The Hobbit", AuthorId = 3 },
            new Book { Id = 6, Title = "The Lord of the Rings", AuthorId = 3 }
            );
        base.OnModelCreating(modelBuilder);
    }
}
