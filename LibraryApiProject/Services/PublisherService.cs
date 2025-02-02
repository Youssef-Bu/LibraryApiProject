namespace LibraryApiProject.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class PublisherService : IPublisherService
{
    private readonly LibraryContext _context;
    private readonly ILogger<PublisherService> _logger;

    public PublisherService(LibraryContext context, ILogger<PublisherService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<Publisher>> GetPublishersAsync()
    {
        try
        {
            return await _context.Publishers.ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetPublishersAsync");
            throw;
        }
    }

    public async Task<Publisher> CreatePublisherAsync(Publisher publisher)
    {
        try
        {
            _context.Publishers.Add(publisher);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Publisher created with id {Id}", publisher.Id);
            return publisher;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in CreatePublisherAsync");
            throw;
        }
    }

    public async Task<bool> DeletePublisherAsync(int id)
    {
        try
        {
            var publisher = await _context.Publishers.FindAsync(id);
            if (publisher == null)
                return false;
            bool hasBooks = await _context.Books.AnyAsync(b => b.PublisherId == id);
            if (hasBooks)
                return false;
            _context.Publishers.Remove(publisher);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Publisher deleted with id {Id}", id);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in DeletePublisherAsync");
            throw;
        }
    }
}

