namespace SolidDemo.Interfaces;

internal interface ICurrentAccount : IAccount
{
    decimal OverDraft { get; }
}