using SolidDemo.BankAccounts.Interfaces;
using SolidDemo.LoanAccounts.Interfaces;

namespace SolidDemo;

internal class Customer(int customerId, string name, IReadOnlyList<IAccount> accounts, IReadOnlyList<ILoan> loans)
{
    public int CustomerId { get; } = customerId;

    public string Name { get; } = name;

    public IReadOnlyList<IAccount> Accounts { get; } = accounts;
    public IReadOnlyList<ILoan> Loans { get; } = loans;

    public IAccount GetBankAccount(int accountId) => Accounts.First(a => a.AccountId == accountId);
    public ILoan GetLoanAccount(int loanId) => Loans.First(a => a.LoanId == loanId);
}

