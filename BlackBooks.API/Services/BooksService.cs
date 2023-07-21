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
        this.ValidateBookDTO(book);

        

        var dbBook = _dbContext.Books
                                .Include(b => b.Categories)
                                .First(b => b.Id == book.Id);

        dbBook.AuthorId = book.AuthorId;
        dbBook.Title    = book.Title.Trim();
        this.UpdateBookCategories(ref dbBook, book.CategoryIds.ToArray());
        
        _dbContext.SaveChanges();

        return new BookDTO
        {
            Id          = dbBook.Id,
            AuthorId    = dbBook.AuthorId,
            Title       = dbBook.Title,
            CategoryIds = dbBook.Categories.Select(c => c.CategoryId).ToList()
        };
    }


    private void ValidateBookDTO(BookDTO book)
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
        bool authorExists = this._dbContext.Authors.Any(a => a.Id == book.AuthorId);
        if (!authorExists)
            throw new ArgumentException("Author does not exist", nameof(book.AuthorId));

    }
    private void UpdateBookCategories(ref Book dbBook, int[] categoryIds)
    {
        dbBook.Categories.Clear();

        List<Category> selectedCategories = _dbContext.Categories.Where(c => categoryIds.Contains(c.Id)).ToList();

        foreach (var category in selectedCategories)
        {
            dbBook.Categories.Add(new BookCategory { Category = category, BookId = dbBook.Id });
        }
    }
}
