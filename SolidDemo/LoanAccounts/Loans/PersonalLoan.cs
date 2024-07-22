using SolidDemo.LoanAccounts.Enums;
using SolidDemo.LoanAccounts.Interfaces;

namespace SolidDemo.LoanAccounts.Loans
{
    internal class PersonalLoan(int accountId, decimal loanAmount, int duration) : Loan(accountId, loanAmount, 0.02, duration), IPersonalLoan
    {
        public LoanType LoanType => LoanType.PersonalLoan;
    }
}
