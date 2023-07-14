using Microsoft.AspNetCore.Mvc;

namespace BlackBooks.API.Controllers;

[ApiController]
[Route("[controller]")]
public class BooksController : ControllerBase
{
    private readonly ILogger<BooksController> _logger;

    public BooksController(ILogger<BooksController> logger)
    {
        this._logger = logger;
    }
    
    [HttpGet("All")]
    public ActionResult<List<string>> GetAll()
    {
        return new List<string>
        { 
            "Book 1", 
            "Book 2",
            "Book 3",
            "Book 4"
        };
    }

    [HttpGet("Search/{searchTerm}")]
    public ActionResult<List<string>> Search(string searchTerm)
    {
        return new List<string>
        { 
            $"Book 1 containing { searchTerm }", 
            $"Book 2 containing { searchTerm }"
        };
    }
}