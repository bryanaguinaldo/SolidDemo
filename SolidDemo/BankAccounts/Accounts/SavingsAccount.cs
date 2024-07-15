using SolidDemo.BankAccounts.Enums;
using SolidDemo.BankAccounts.Interfaces;

namespace SolidDemo.BankAccounts.Accounts;

internal class SavingsAccount(int accountId, decimal balance) : Account(accountId, balance), IAccount
{
    public AccountType AccountType => AccountType.Savings;
}
