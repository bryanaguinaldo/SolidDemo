namespace SolidDemo.BankAccounts.Interfaces;

internal interface ITimeDepositAccount : IAccount
{
    bool IsMatured();
}