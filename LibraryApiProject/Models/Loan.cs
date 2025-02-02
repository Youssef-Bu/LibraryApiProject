using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Loan
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("Book")]
    public int BookId { get; set; }
    public Book Book { get; set; }

    [Required]
    public string BorrowerName { get; set; }

    public DateTime BorrowDate { get; set; } = DateTime.Now;

    public DateTime? ReturnDate { get; set; }
}

