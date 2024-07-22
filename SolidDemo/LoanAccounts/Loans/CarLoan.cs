using SolidDemo.LoanAccounts.Enums;
using SolidDemo.LoanAccounts.Interfaces;

namespace SolidDemo.LoanAccounts.Loans
{
    public class CarLoan(int accountId, decimal loanAmount, decimal duration) : Loan(accountId, LoanType.CarLoan, loanAmount, 5, duration), ICarLoan
    {
        
    }
}
