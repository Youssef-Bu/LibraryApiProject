namespace LibraryApiProject.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class LoanService : ILoanService
{
    private readonly LibraryContext _context;
    private readonly ILogger<LoanService> _logger;

    public LoanService(LibraryContext context, ILogger<LoanService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Loan> CreateLoanAsync(Loan loan)
    {
        try
        {
            _context.Loans.Add(loan);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Loan created with id {Id}", loan.Id);
            return loan;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in CreateLoanAsync");
            throw;
        }
    }

    public async Task<Loan> ReturnLoanAsync(int id)
    {
        try
        {
            var loan = await _context.Loans.FindAsync(id);
            if (loan == null)
                return null;
            loan.ReturnDate = DateTime.Now;
            await _context.SaveChangesAsync();
            _logger.LogInformation("Loan returned with id {Id}", id);
            return loan;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in ReturnLoanAsync");
            throw;
        }
    }

    public async Task<IEnumerable<Loan>> GetLoansAsync(int? bookId, string borrowerName, DateTime? date)
    {
        try
        {
            var query = _context.Loans.Include(l => l.Book).AsQueryable();
            if (bookId.HasValue)
            {
                query = query.Where(l => l.BookId == bookId.Value);
            }
            if (!string.IsNullOrEmpty(borrowerName))
            {
                query = query.Where(l => l.BorrowerName.Contains(borrowerName));
            }
            if (date.HasValue)
            {
                query = query.Where(l => l.BorrowDate.Date == date.Value.Date);
            }
            return await query.ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetLoansAsync");
            throw;
        }
    }
}

