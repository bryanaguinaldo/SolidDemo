using SolidDemo.BankAccounts.Interfaces;
using SolidDemo.LoanAccounts.Interfaces;

namespace SolidDemo;

public class Customer(int customerId, string name, List<IAccount> accounts, List<ILoan> loans)
{
    public int CustomerId { get; } = customerId;

    public string Name { get; } = name;

    public List<IAccount> Accounts { get; } = accounts;
    public List<ILoan> Loans { get; } = loans;

    public IAccount GetBankAccount(int accountId) => Accounts.First(a => a.AccountId == accountId);
    public ILoan GetLoanAccount(int loanId) => Loans.First(a => a.LoanId == loanId);
}

