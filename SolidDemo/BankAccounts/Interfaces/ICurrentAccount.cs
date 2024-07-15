namespace SolidDemo.BankAccounts.Interfaces;

internal interface ICurrentAccount : IAccount
{
    decimal OverDraft { get; }
}