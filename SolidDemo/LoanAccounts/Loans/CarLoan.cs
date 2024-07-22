using SolidDemo.LoanAccounts.Enums;
using SolidDemo.LoanAccounts.Interfaces;

namespace SolidDemo.LoanAccounts.Loans
{
    public class CarLoan(int accountId, decimal loanAmount, int duration) : Loan(accountId, LoanType.CarLoan, loanAmount, 0.02, duration), ICarLoan
    {
        
    }
}
