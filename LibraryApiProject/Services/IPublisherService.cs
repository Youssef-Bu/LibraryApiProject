namespace LibraryApiProject.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryApiProject.Models;

public interface IPublisherService
{
    Task<IEnumerable<Publisher>> GetPublishersAsync();
    Task<Publisher> CreatePublisherAsync(Publisher publisher);
    Task<bool> DeletePublisherAsync(int id);
}

