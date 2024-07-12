namespace SolidDemo.Accounts;

internal class SavingsAccount(int accountId, decimal balance) : Account(accountId, balance), IAccount
{
    public AccountType AccountType => AccountType.Savings;
}
