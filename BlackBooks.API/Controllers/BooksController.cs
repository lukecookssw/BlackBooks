using BlackBooks.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlackBooks.API.Controllers;

[ApiController]
[Route("[controller]")]
public class BooksController : ControllerBase
{
    private readonly BooksService _booksService;
    private readonly ILogger<BooksController> _logger;
    
    public BooksController(ILogger<BooksController> logger, BooksService booksService)
    {
        this._booksService  = booksService;
        this._logger        = logger;
    }
    
    [HttpGet("All")]
    public ActionResult<List<string>> GetAll()
    {
        try
        {
            List<string> results = this._booksService.GetAllBooks();
            return results;
        }
        catch (Exception ex)
        {

            this._logger.LogError(ex, "GetAll");
            return StatusCode(500, "Something went wrong");
        }
        
    }

    [HttpGet("Search/{searchTerm}")]
    public ActionResult<List<string>> Search(string searchTerm)
    {
        try
        {
            List<string> results = this._booksService.SearchBooks(searchTerm);
            return results;
        }
        catch(Exception ex)
        {
            this._logger.LogError(ex, "Search");
            return StatusCode(500, "Something went wrong");
        }
    }
}