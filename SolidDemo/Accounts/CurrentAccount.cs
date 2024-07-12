namespace SolidDemo.Accounts;
internal class CurrentAccount(int accountId, decimal balance, decimal overDraft) :
    Account(accountId, balance), ICurrentAccount
{
    public decimal OverDraft { get; } = overDraft;

    public AccountType AccountType => AccountType.Current;
}
