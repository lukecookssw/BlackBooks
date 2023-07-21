using BlackBooks.API.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlackBooks.API.Data;

public class BlackBooksDbContext : DbContext
{
    public DbSet<Category> Categories => this.Set<Category>();
    public DbSet<Book> Books => this.Set<Book>();
    public DbSet<BookCategory> BookCategories => this.Set<BookCategory>();
    public DbSet<Author> Authors => this.Set<Author>();
    
    public BlackBooksDbContext(DbContextOptions<BlackBooksDbContext> options) 
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // TODO: Create Migration and apply these to the database.
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Fiction" },
            new Category { Id = 2, Name = "Non-Fiction" },
            new Category { Id = 3, Name = "Science Fiction" },
            new Category { Id = 4, Name = "Fantasy" },
            new Category { Id = 5, Name = "Horror" },
            new Category { Id = 6, Name = "Thriller" }
            );
        
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

        modelBuilder.Entity<BookCategory>().HasData(
            new BookCategory { Id = 1, BookId = 1, CategoryId = 1 },
            new BookCategory { Id = 2, BookId = 1, CategoryId = 3 },
            new BookCategory { Id = 3, BookId = 2, CategoryId = 1 },
            new BookCategory { Id = 4, BookId = 2, CategoryId = 3 },
            new BookCategory { Id = 5, BookId = 3, CategoryId = 1 },
            new BookCategory { Id = 6, BookId = 3, CategoryId = 3 },
            new BookCategory { Id = 7, BookId = 4, CategoryId = 1 },
            new BookCategory { Id = 8, BookId = 4, CategoryId = 3 },
            new BookCategory { Id = 9, BookId = 5, CategoryId = 1 },
            new BookCategory { Id = 10, BookId = 5, CategoryId = 4 },
            new BookCategory { Id = 11, BookId = 6, CategoryId = 1 },
            new BookCategory { Id = 12, BookId = 6, CategoryId = 4 }
            );
        
        base.OnModelCreating(modelBuilder);
    }
}
