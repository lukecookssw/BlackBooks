using BlackBooks.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlackBooks.API.Controllers;
// TODO: Async all the things
[ApiController]
[Route("[controller]")]
public class AuthorsController : ControllerBase
{
    private readonly AuthorsService _authorsService;
    private readonly ILogger<AuthorsController> _logger;

    public AuthorsController(ILogger<AuthorsController> logger, AuthorsService authorsService)
    {
        this._authorsService = authorsService;
        this._logger = logger;
    }

    [HttpGet("All")]
    public ActionResult<List<string>> GetAll()
    {
        try
        {
            List<string> results = this._authorsService.GetAllAuthors();
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
            List<string> results = this._authorsService.SearchAuthors(searchTerm);
            return results;
        }
        catch (Exception ex)
        {
            this._logger.LogError(ex, "Search");
            return StatusCode(500, "Something went wrong");
        }
    }
}