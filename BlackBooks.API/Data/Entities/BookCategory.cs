﻿namespace BlackBooks.API.Data.Entities;

public class BookCategory
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public int CategoryId { get; set; }
    public Book Book { get; set; } = null!;
    public Category Category { get; set; } = null!;
}
