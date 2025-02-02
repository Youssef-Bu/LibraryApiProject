using LibraryApiProject.Models;

using LibraryApiProject.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

/// <summary>
/// Controller for managing loans.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class LoansController : ControllerBase
{
    private readonly ILoanService _loanService;

    public LoansController(ILoanService loanService)
    {
        _loanService = loanService;
    }

    /// <summary>
    /// Create a new loan.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateLoan([FromBody] Loan loan)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var createdLoan = await _loanService.CreateLoanAsync(loan);
        return CreatedAtAction(nameof(GetLoans), new { id = createdLoan.Id }, createdLoan);
    }

    /// <summary>
    /// Record the return of a loan.
    /// </summary>
    [HttpPut("{id}/return")]
    public async Task<IActionResult> ReturnLoan(int id)
    {
        var updatedLoan = await _loanService.ReturnLoanAsync(id);
        if (updatedLoan == null)
            return NotFound();
        return Ok(updatedLoan);
    }

    /// <summary>
    /// Get all loans with optional filtering.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetLoans([FromQuery] int? bookId, [FromQuery] string borrowerName, [FromQuery] DateTime? date)
    {
        var loans = await _loanService.GetLoansAsync(bookId, borrowerName, date);
        return Ok(loans);
    }
}

