namespace SolidDemo.Accounts;

internal interface ITimeDepositAccount : IAccount
{
    bool IsMatured();
}