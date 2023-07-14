using BlackBooks.API.Data;

namespace BlackBooks.API.Services;

public class AuthorsService
{
    private readonly BlackBooksDbContext _dbContext;

    public AuthorsService(BlackBooksDbContext dbContext)
    {
        this._dbContext = dbContext;
    }
    
    public List<string> GetAllAuthors()
    {
        List<string> authors = this._dbContext.Authors
                                .Select(a => a.Name)
                                .ToList();
        return authors;
    }

    public List<string> SearchAuthors(string searchTerm)
    {
        if (String.IsNullOrWhiteSpace(searchTerm))
        {
            throw new ArgumentNullException(nameof(searchTerm));
        }

        searchTerm = searchTerm
                        .Trim()
                        .ToLower();

        List<string> authors = this._dbContext.Authors
                                .Where(a => a.Name.ToLower().Contains(searchTerm))
                                .Select(a => a.Name)
                                .ToList();

        return authors;
    }
}
