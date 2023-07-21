using System.ComponentModel.DataAnnotations;

namespace BlackBooks.API.Data.Entities;

public class Book
{
    [Key]
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    // TODO: Add Author
}
