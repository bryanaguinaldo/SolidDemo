using SolidDemo.LoanAccounts.Enums;
using SolidDemo.LoanAccounts.Interfaces;

namespace SolidDemo.LoanAccounts.Loans
{
    internal class HomeLoan(int accountId, decimal loanAmount, int duration) : Loan(accountId, loanAmount, duration), IHomeLoan
    {
        public LoanType LoanType => LoanType.PersonalLoan;
        public override double InterestRate => 0.02;
    }
}
