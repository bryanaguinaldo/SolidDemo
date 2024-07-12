namespace SolidDemo.Accounts;

internal class TimeDepositAccount(int accountId, decimal balance, DateTime dateCreated, int period) :
    Account(accountId, balance), ITimeDepositAccount
{
    private readonly DateTime _dateCreated = dateCreated;

    private readonly TimeSpan _time = TimeSpan.FromDays(period);

    public AccountType AccountType => AccountType.TimeDeposit;

    public bool IsMatured() => DateTime.Now.Subtract(_dateCreated).TotalDays > _time.TotalDays;
}
