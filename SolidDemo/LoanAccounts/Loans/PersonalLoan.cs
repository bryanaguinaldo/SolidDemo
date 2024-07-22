using SolidDemo.LoanAccounts.Enums;
using SolidDemo.LoanAccounts.Interfaces;

namespace SolidDemo.LoanAccounts.Loans
{
    internal class PersonalLoan(int accountId, decimal loanAmount, decimal duration) : Loan(accountId, LoanType.PersonalLoan, loanAmount, 2, duration), IPersonalLoan
    {

    }
}
