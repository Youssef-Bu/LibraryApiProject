using LibraryApiProject.Models;

using LibraryApiProject.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

/// <summary>
/// Controller for managing publishers.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class PublishersController : ControllerBase
{
    private readonly IPublisherService _publisherService;

    public PublishersController(IPublisherService publisherService)
    {
        _publisherService = publisherService;
    }

    /// <summary>
    /// Get all publishers.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetPublishers()
    {
        var publishers = await _publisherService.GetPublishersAsync();
        return Ok(publishers);
    }

    /// <summary>
    /// Create a new publisher.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreatePublisher([FromBody] Publisher publisher)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var createdPublisher = await _publisherService.CreatePublisherAsync(publisher);
        return CreatedAtAction(nameof(GetPublishers), new { id = createdPublisher.Id }, createdPublisher);
    }

    /// <summary>
    /// Delete a publisher if no book is associated.
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePublisher(int id)
    {
        var result = await _publisherService.DeletePublisherAsync(id);
        if (!result)
            return BadRequest("Publisher cannot be deleted or not found.");
        return NoContent();
    }
}

