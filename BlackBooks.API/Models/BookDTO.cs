using BlackBooks.API.Data.Entities;

namespace BlackBooks.API.Models;

public class BookDTO
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int AuthorId { get; set; }
    public ICollection<int> CategoryIds { get; set; } = new HashSet<int>();
}
