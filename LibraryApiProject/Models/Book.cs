using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LibraryApiProject.Models;

public class Book
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(200)]
    public string Title { get; set; }

    [Required]
    [StringLength(100)]
    public string Author { get; set; }

    [Range(1500, int.MaxValue)]
    public int PublishedYear { get; set; }

    [Required]
    public string ISBN { get; set; }

    [ForeignKey("Category")]
    public int CategoryId { get; set; }
    public Category Category { get; set; }

    [ForeignKey("Publisher")]
    public int PublisherId { get; set; }
    public Publisher Publisher { get; set; }
}

