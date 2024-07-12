namespace SolidDemo.Accounts;

internal interface ICurrentAccount : IAccount
{
    decimal OverDraft { get; }
}