namespace LibraryApiProject.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryApiProject.Models;

public interface ILoanService
{
    Task<Loan> CreateLoanAsync(Loan loan);
    Task<Loan> ReturnLoanAsync(int id);
    Task<IEnumerable<Loan>> GetLoansAsync(int? bookId, string borrowerName, DateTime? date);
}

