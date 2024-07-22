using SolidDemo.LoanAccounts.Enums;
using SolidDemo.LoanAccounts.Interfaces;

namespace SolidDemo.LoanAccounts.Loans
{
    public class HomeLoan(int accountId, decimal loanAmount, decimal duration) : Loan(accountId, LoanType.PersonalLoan, loanAmount, 7, duration), IHomeLoan
    {

    }
}
