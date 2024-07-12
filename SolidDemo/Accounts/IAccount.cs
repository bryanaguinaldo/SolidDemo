namespace SolidDemo.Accounts;

internal interface IAccount
{
    int AccountId { get; }
    decimal Balance { get; set; }
    void Deposit(decimal amount);
    void Withdraw(decimal amount);
    AccountType AccountType { get; }
}