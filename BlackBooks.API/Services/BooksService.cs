namespace BlackBooks.API.Services;

public class BooksService
{
    // TODO: Add DbContext here
    // private readonly BlackBooksDbContext _dbContext;
    public BooksService()
    {
        // TODO: Inject DbContext here
    }

    public List<string> GetAllBooks()
    {
        // TODO: Get all books from the database
        return new List<string>
        {
            "Book 1",
            "Book 2",
            "Book 3",
            "Book 4"
        };
    }
    
    public List<string> SearchBooks(string searchTerm)
    {
        // TODO: Search the database for books where their title contains the search term
        return new List<string>
        {
            $"Book 1 containing { searchTerm }",
            $"Book 2 containing { searchTerm }"
        };
    }
}
