using System.ComponentModel.DataAnnotations;

namespace BlackBooks.API.Data.Entities;

public class Category
{
    [Key]
    public string Name { get; set; } = string.Empty;

    public ICollection<BookCategory> BooksInCategory { get; } = new HashSet<BookCategory>();
}
