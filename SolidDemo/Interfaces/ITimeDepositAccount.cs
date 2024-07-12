namespace SolidDemo.Interfaces;

internal interface ITimeDepositAccount : IAccount
{
    bool IsMatured();
}