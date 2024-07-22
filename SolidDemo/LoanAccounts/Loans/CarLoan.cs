using SolidDemo.LoanAccounts.Enums;
using SolidDemo.LoanAccounts.Interfaces;

namespace SolidDemo.LoanAccounts.Loans
{
    internal class CarLoan(int accountId, decimal loanAmount, int duration) : Loan(accountId, loanAmount, 0.02, duration), ICarLoan
    {
        public LoanType LoanType => LoanType.CarLoan;
    }
}
