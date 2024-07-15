using SolidDemo.LoanAccounts.Enums;
using SolidDemo.LoanAccounts.Interfaces;

namespace SolidDemo.LoanAccounts.Loans
{
    internal class PersonalLoan(int accountId, decimal loanAmount, int duration) : Loan(accountId, loanAmount, duration), IPersonalLoan
    {
        public LoanType LoanType => LoanType.PersonalLoan;
        public override double InterestRate => 0.02;
    }
}
