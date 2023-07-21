using BlackBooks.API.Data;
using BlackBooks.API.Data.Entities;
using BlackBooks.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BlackBooks.API.Services;

public class BooksService
{
    private readonly BlackBooksDbContext _dbContext;
    public BooksService(BlackBooksDbContext dbContext)
    {
        this._dbContext = dbContext;
    }

    public List<string> GetAllBooks()
    {
        List<string> titles = this._dbContext.Books
                                .Select(b => b.Title)
                                .ToList();
        return titles;
    }
    
    public List<string> SearchBooks(string searchTerm)
    {
        if (String.IsNullOrWhiteSpace(searchTerm))
        {
            throw new ArgumentNullException(nameof(searchTerm));
        }
        searchTerm = searchTerm
                        .Trim()
                        .ToLower();

        List<string> titles = this._dbContext.Books
                                .Where(b => 
                                        b.Title.ToLower().Contains(searchTerm)
                                    ||  b.Categories.Any(c => c.Category.Name.ToLower().Contains(searchTerm))
                                    )
                                .Select(b => b.Title)
                                .ToList();

        return titles;
    }

    public BookDTO UpdateBook(BookDTO book)
    {
        var dbBook = _dbContext.Books.Include(b => b.Categories).First(b => b.Id == book.Id);
        dbBook.AuthorId = book.AuthorId;
        dbBook.Title    = book.Title.Trim();
        dbBook.Categories.Clear();

        List<Category> selectedCategories = _dbContext.Categories.Where(c => book.CategoryIds.Contains(c.Id)).ToList();

        foreach (var category in selectedCategories)
        {
            dbBook.Categories.Add(new BookCategory { Category = category, BookId = dbBook.Id });
        }
        _dbContext.SaveChanges();

        return new BookDTO
        {
            Id = dbBook.Id,
            AuthorId = dbBook.AuthorId,
            Title = dbBook.Title,
            CategoryIds = dbBook.Categories.Select(c => c.CategoryId).ToList()
        };
    }

    
}
