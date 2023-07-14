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
        modelBuilder.Entity<Category>().HasData(
            new Category { Name = "Fiction" },
            new Category { Name = "Non-Fiction" },
            new Category { Name = "Science Fiction" },
            new Category { Name = "Fantasy" },
            new Category { Name = "Horror" },
            new Category { Name = "Thriller" }
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
            new BookCategory { Id = 1, BookId = 1, CategoryId = "Fiction" },
            new BookCategory { Id = 2, BookId = 1, CategoryId = "Science Fiction" },
            new BookCategory { Id = 3, BookId = 2, CategoryId = "Fiction" },
            new BookCategory { Id = 4, BookId = 2, CategoryId = "Science Fiction" },
            new BookCategory { Id = 5, BookId = 3, CategoryId = "Fiction" },
            new BookCategory { Id = 6, BookId = 3, CategoryId = "Science Fiction" },
            new BookCategory { Id = 7, BookId = 4, CategoryId = "Fiction" },
            new BookCategory { Id = 8, BookId = 4, CategoryId = "Science Fiction" },
            new BookCategory { Id = 9, BookId = 5, CategoryId = "Fiction" },
            new BookCategory { Id = 10, BookId = 5, CategoryId = "Fantasy" },
            new BookCategory { Id = 11, BookId = 6, CategoryId = "Fiction" },
            new BookCategory { Id = 12, BookId = 6, CategoryId = "Fantasy" }
            );
        
        base.OnModelCreating(modelBuilder);
    }
}
