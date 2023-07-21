using BlackBooks.API.Data;
using BlackBooks.API.Data.Entities;
using BlackBooks.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BlackBooks.API.Services;
// TODO: Add telemetry logging (method runtime) that can be enabled/disabled via a config setting
public class BooksService
{
    private readonly BlackBooksDbContext _dbContext;
    public BooksService(BlackBooksDbContext dbContext)
    {
        this._dbContext = dbContext;
    }

    public async Task<List<string>> GetAllBooks()
    {
        List<string> titles = await this._dbContext.Books
                                .Select(b => b.Title)
                                .ToListAsync();
        return titles;
    }
    
    public async Task<List<string>> SearchBooks(string searchTerm)
    {
        if (String.IsNullOrWhiteSpace(searchTerm))
        {
            return new List<string>();
        }
        searchTerm = searchTerm
                        .Trim()
                        .ToLower();

        List<string> titles = await this._dbContext.Books
                                .Where(b => 
                                        b.Title.ToLower().Contains(searchTerm)
                                    ||  b.Categories.Any(c => c.Category.Name.ToLower().Contains(searchTerm))
                                    )
                                .Select(b => b.Title)
                                .ToListAsync();

        return titles;
    }

    public async Task<BookDTO> UpdateBook(BookDTO book)
    {
        await this.ValidateBookDTO(book);

        var dbBook = await _dbContext.Books
                                .Include(b => b.Categories)
                                .FirstAsync(b => b.Id == book.Id);

        dbBook.AuthorId = book.AuthorId;
        dbBook.Title    = book.Title.Trim();
        await this.UpdateBookCategories(dbBook, book.CategoryIds.ToArray());
        
        await _dbContext.SaveChangesAsync();

        return new BookDTO
        {
            Id          = dbBook.Id,
            AuthorId    = dbBook.AuthorId,
            Title       = dbBook.Title,
            CategoryIds = dbBook.Categories.Select(c => c.CategoryId).ToList()
        };
    }


    private async Task ValidateBookDTO(BookDTO book)
    {
        if (String.IsNullOrWhiteSpace(book.Title)) 
            throw new ArgumentException("Must have a title", nameof(book.Title));
        if (book.AuthorId <= 0) 
            throw new ArgumentException("Must have an author", nameof(book.AuthorId));
        if (!book.CategoryIds.Any()) 
            throw new ArgumentException("Must have at least 1 category", nameof(book.CategoryIds));

        bool duplicateOrZeroCategoryIds = book.CategoryIds.Any(c => c <= 0 || book.CategoryIds.Count(c2 => c2 == c) > 1);
        if (duplicateOrZeroCategoryIds) 
            throw new ArgumentException("CategoryIds must be unique and greater than 0", nameof(book.CategoryIds));

        // Expensive to do a whole round trip to the DB to validate the author... :(
        bool authorExists = await this._dbContext.Authors.AnyAsync(a => a.Id == book.AuthorId);
        if (!authorExists)
            throw new ArgumentException("Author does not exist", nameof(book.AuthorId));

        // Expensive to do a whole round trip to the DB to validate the categories... :(
        book.CategoryIds = book.CategoryIds.Distinct().ToList();
        bool categoriesExist = await this._dbContext.Categories
                                    .Where(c => book.CategoryIds.Contains(c.Id))
                                    .CountAsync() == book.CategoryIds.Count;
        if (!categoriesExist)
            throw new ArgumentException("One or more categories do not exist", nameof(book.CategoryIds));

    }
    private async Task UpdateBookCategories(Book dbBook, int[] categoryIds)
    {
        dbBook.Categories.Clear();

        List<Category> selectedCategories = await _dbContext.Categories
                                                        .Where(c => categoryIds.Contains(c.Id))
                                                        .Distinct()
                                                        .ToListAsync();

        // iterate only over the valid (database-fetched) categories
        foreach (var category in selectedCategories)
        {
            dbBook.Categories.Add(new BookCategory { Category = category, BookId = dbBook.Id });
        }
    }
}
