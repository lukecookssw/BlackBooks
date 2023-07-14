using BlackBooks.API.Data;

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
                                .Where(b => b.Title.ToLower().Contains(searchTerm))
                                .Select(b => b.Title)
                                .ToList();

        return titles;
    }
}
