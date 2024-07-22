using SolidDemo.LoanAccounts.Enums;
using SolidDemo.LoanAccounts.Interfaces;

namespace SolidDemo.LoanAccounts.Loans
{
    internal class HomeLoan(int accountId, decimal loanAmount, int duration) : Loan(accountId, LoanType.PersonalLoan, loanAmount, 0.02, duration), IHomeLoan
    {

    }
}
