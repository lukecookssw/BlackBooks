using BlackBooks.API.Data;
using Microsoft.EntityFrameworkCore;

namespace BlackBooks.API.Services;
public class AuthorsService
{
    private readonly BlackBooksDbContext _dbContext;

    public AuthorsService(BlackBooksDbContext dbContext)
    {
        this._dbContext = dbContext;
    }
    
    public async Task<List<string>> GetAllAuthors()
    {
        List<string> authors = await this._dbContext.Authors
                                .Select(a => a.Name)
                                .ToListAsync();
        return authors;
    }

    public async Task<List<string>> SearchAuthors(string searchTerm)
    {
        if (String.IsNullOrWhiteSpace(searchTerm))
        {
            return new List<string>();
        }

        searchTerm = searchTerm
                        .Trim()
                        .ToLower();

        List<string> authors = await this._dbContext.Authors
                                .Where(a => a.Name.ToLower().Contains(searchTerm))
                                .Select(a => a.Name)
                                .ToListAsync();

        return authors;
    }
}
