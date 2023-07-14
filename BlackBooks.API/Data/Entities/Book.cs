using System.ComponentModel.DataAnnotations;

namespace BlackBooks.API.Data.Entities;

public class Book
{
    [Key]
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int AuthorId { get; set; }

    public Author Author { get; set; } = null!;
    public ICollection<BookCategory> Categories { get; } = new HashSet<BookCategory>();
}
