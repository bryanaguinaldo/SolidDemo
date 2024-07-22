using SolidDemo.BankAccounts.Enums;

namespace SolidDemo.BankAccounts.Interfaces;

public interface IAccount
{
    int AccountId { get; }
    decimal Balance { get; set; }
    void Deposit(decimal amount);
    void Withdraw(decimal amount);
    AccountType AccountType { get; }
}