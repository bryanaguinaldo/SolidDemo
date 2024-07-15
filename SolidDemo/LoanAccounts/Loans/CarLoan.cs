using SolidDemo.LoanAccounts.Enums;
using SolidDemo.LoanAccounts.Interfaces;

namespace SolidDemo.LoanAccounts.Loans
{
    internal class CarLoan(int accountId, decimal loanAmount, int duration) : Loan(accountId, loanAmount, duration), ICarLoan
    {
        public LoanType LoanType => LoanType.CarLoan;
        public override double InterestRate => 0.02;
    }
}
