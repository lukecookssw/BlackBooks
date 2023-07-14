using BlackBooks.API.Data;
using BlackBooks.API.Data.Entities;

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
        // TODO: Fix warning
        List<Book> books = this._dbContext.Books
                                .ToList();
        List<string> titles = books
                                .Where(b => b.Title != null)
                                .Select(b => b.Title)
                                .ToList();

        return titles;
    }
    
    public List<string> SearchBooks(string searchTerm)
    {
        // TODO: Add validation and sanitisation
        
        List<Book> books = this._dbContext.Books
                                .ToList();

        // TODO: Fix warning
        List<string> titles = books
                                .Where(b => b.Title.Contains(searchTerm))
                                .Select(b => b.Title)
                                .ToList();

        return titles;
    }
}
