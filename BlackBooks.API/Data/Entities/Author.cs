﻿namespace BlackBooks.API.Data.Entities;

public class Author
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<Book> Books { get; } = new HashSet<Book>();
}