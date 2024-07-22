using SolidDemo.BankAccounts.Enums;
using SolidDemo.BankAccounts.Interfaces;

namespace SolidDemo.BankAccounts.Accounts;

public class SavingsAccount(int accountId, decimal balance) : Account(accountId, balance), IAccount
{
    public AccountType AccountType => AccountType.Savings;
}
