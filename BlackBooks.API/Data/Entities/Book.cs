using System.ComponentModel.DataAnnotations;

namespace BlackBooks.API.Data.Entities;

public class Book
{
    [Key]
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int AuthorId { get; set; }

    // TODO: Fix warning
    public Author Author { get; set; }

    // TODO: ADD - public ICollection<Category> Categories { get; set; } = new HashSet<Category>();
}
