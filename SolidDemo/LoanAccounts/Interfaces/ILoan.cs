using SolidDemo.LoanAccounts.Enums;

namespace SolidDemo.LoanAccounts.Interfaces;

public interface ILoan
{
    int LoanId { get; }
    decimal TotalAmount();
    decimal MonthlyAmortization();
    int Duration { get; }
    LoanType LoanType { get; }
    void OutputMessage();
}