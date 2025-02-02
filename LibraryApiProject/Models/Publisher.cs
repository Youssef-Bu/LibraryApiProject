using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Publisher
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    [Required]
    [EmailAddress]
    public string ContactEmail { get; set; }

    [Range(1801, int.MaxValue)]
    public int? FoundedYear { get; set; }

    public ICollection<Book> Books { get; set; }
}

