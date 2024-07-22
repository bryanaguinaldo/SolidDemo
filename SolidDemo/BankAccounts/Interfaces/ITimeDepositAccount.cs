namespace SolidDemo.BankAccounts.Interfaces;

public interface ITimeDepositAccount : IAccount
{
    bool IsMatured();
}