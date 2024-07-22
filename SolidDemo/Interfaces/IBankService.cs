using SolidDemo.BankAccounts.Enums;

namespace SolidDemo.Interfaces;

internal interface IBankService
{
    void Withdraw(Customer customer, int accountId, decimal amount);
    void Deposit(Customer customer, int accountId, decimal amount);
}