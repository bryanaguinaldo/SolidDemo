using SolidDemo.LoanAccounts.Interfaces;

namespace SolidDemo.Interfaces;

public interface ILoanService
{

    void AddLoan(Customer customer, ILoan loan);
    void DisplayLoanDetails(Customer customer);
}