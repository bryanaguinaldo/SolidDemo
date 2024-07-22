namespace SolidDemo.Interfaces;

public interface IBankService
{
    void Withdraw(Customer customer, int accountId, decimal amount);
    void Deposit(Customer customer, int accountId, decimal amount);
}