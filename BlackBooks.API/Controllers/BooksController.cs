using Microsoft.AspNetCore.Mvc;

namespace BlackBooks.API.Controllers;

[ApiController]
[Route("[controller]")]
public class BooksController : ControllerBase
{
    // TODO: Add private Services here, e.g.:
    // private readonly SomeService _someService;

    // TODO: Implement logging
    private readonly ILogger<BooksController> _logger;
    
    public BooksController(ILogger<BooksController> logger)
    {
        // TODO: Inject Services here, e.g.:
        // this._someService = someService;
        this._logger = logger;
    }
    
    [HttpGet("All")]
    public ActionResult<List<string>> GetAll()
    {
        // TODO: Move this to BooksService
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
        // TODO: Move this to BooksService
        return new List<string>
        { 
            $"Book 1 containing { searchTerm }", 
            $"Book 2 containing { searchTerm }"
        };
    }
}