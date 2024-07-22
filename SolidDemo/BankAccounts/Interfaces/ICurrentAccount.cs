namespace SolidDemo.BankAccounts.Interfaces;

public interface ICurrentAccount : IAccount
{
    decimal OverDraft { get; }
}