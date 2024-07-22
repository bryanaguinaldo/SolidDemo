using SolidDemo.LoanAccounts.Enums;

namespace SolidDemo.LoanAccounts.Interfaces;

public interface ILoan
{
    int LoanId { get; }
    decimal TotalAmount();
    LoanType LoanType { get; }
}