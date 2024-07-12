namespace SolidDemo;

internal interface IBankService
{
    void Withdraw(Customer customer, int accountId, decimal amount);
}