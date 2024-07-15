namespace SolidDemo.BankAccounts.Interfaces;

internal interface IBankService
{
    void Withdraw(Customer customer, int accountId, decimal amount);
}