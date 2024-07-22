using SolidDemo.BankAccounts.Enums;
using SolidDemo.BankAccounts.Interfaces;

namespace SolidDemo.BankAccounts.Accounts;

internal class DollarAccount(int accountId, decimal balance) : Account(accountId, balance), IDollarAccount
{
    private const decimal DollarToPesoExchangeRate = 60.00m;
    private const decimal InterestRate = 0.02m; //2% Interest Rate Charge

    public AccountType AccountType => AccountType.Dollar;
    public MoneyType MoneyType { get; set; } = MoneyType.Peso;

    public new void Withdraw(decimal amount)
    {
        if (MoneyType == MoneyType.Dollar)
            amount = ConvertDollarToPeso(amount);

        Balance -= amount;
    }

    public new void Deposit(decimal amount)
    {
        if (MoneyType == MoneyType.Dollar)
            amount = ConvertDollarToPeso(amount);

        Balance += GetAmountLessInterest(amount);
    }

    private decimal ConvertDollarToPeso(decimal dollarAmount) => dollarAmount * DollarToPesoExchangeRate;
    private decimal GetAmountLessInterest(decimal amount) => (1 - InterestRate) * amount;
}