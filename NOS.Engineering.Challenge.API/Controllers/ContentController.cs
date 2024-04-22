using System.Net;
using Microsoft.AspNetCore.Mvc;
using NOS.Engineering.Challenge.API.Models;
using NOS.Engineering.Challenge.Managers;

namespace NOS.Engineering.Challenge.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class ContentController : Controller
{
    private readonly IContentsManager _manager;
    private readonly ILogger<ContentController> _logger;

    public ContentController(IContentsManager manager, ILogger<ContentController> logger)
    {
        _manager = manager;
        _logger = logger;
    }
    
    [HttpGet]
    [Obsolete("Replaced by GetContentsByTitleGenre")]
    public async Task<IActionResult> GetManyContents()
    {
        var contents = await _manager.GetManyContents().ConfigureAwait(false);

        if (!contents.Any())
            return NotFound();
        
        return Ok(contents);
    }

    /// <summary>
    /// Gets all contents filtered by title and/or genre
    /// </summary>
    /// <returns>Returns a list of Contents</returns>
    /// <remarks>Replaces GetManyContents</remarks>
    [HttpGet]
    public async Task<IActionResult> GetContentsByTitleGenre(
        [FromBody] ContentInput content)
    {
        var contents = await _manager.GetByTitleGenre(content.ToDto()).ConfigureAwait(false);

        if (!contents.Any())
            return NotFound();

        return Ok(contents);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetContent(Guid id)
    {
        var content = await _manager.GetContent(id).ConfigureAwait(false);

        if (content == null)
            return NotFound();
        
        return Ok(content);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateContent(
        [FromBody] ContentInput content
        )
    {
        var createdContent = await _manager.CreateContent(content.ToDto()).ConfigureAwait(false);

        return createdContent == null ? Problem("Not created") : Ok(createdContent);
    }
    
    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateContent(
        Guid id,
        [FromBody] ContentInput content
        )
    {
        var updatedContent = await _manager.UpdateContent(id, content.ToDto()).ConfigureAwait(false);

        return updatedContent == null ? NotFound() : Ok(updatedContent);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteContent(
        Guid id
    )
    {
        var deletedId = await _manager.DeleteContent(id).ConfigureAwait(false);
        return Ok(deletedId);
    }
    
    [HttpPost("{id}/genre")]
    public Task<IActionResult> AddGenres(
        Guid id,
        [FromBody] IEnumerable<string> genres
    )
    {
        if (_logger.IsDebugEnabled)
        {
            _logger.Debug($"Request Id:", id);
        }        

        var addedGenres = await _manager.AddGenres(id, genres).ConfigureAwait(false);

        if (_logger.IsDebugEnabled)
        {
            //_logger.Debug($"Response:", addedGenres);
        }

        return addedGenres == null ? Problem() : Ok(addedGenres);
    }
    
    [HttpDelete("{id}/genre")]
    public Task<IActionResult> RemoveGenres(
        Guid id,
        [FromBody] IEnumerable<string> genres
    )
    {
        if (_logger.IsDebugEnabled)
        {
            _logger.Debug($"Request Id:", id);
        }

        var removedGenres = await _manager.RemoveGenres(id, genres).ConfigureAwait(false);

        if (_logger.IsDebugEnabled)
        {
            //_logger.Debug($"Response:", removedGenres);
        }

        return removedGenres == null ? Problem() : Ok(removedGenres);
    }
}