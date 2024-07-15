using SolidDemo.BankAccounts.Enums;
using SolidDemo.BankAccounts.Interfaces;

namespace SolidDemo.BankAccounts.Accounts;

internal class DollarAccount(int accountId, decimal balance, MoneyType moneyType) : Account(accountId, balance), IDollarAccount
{
    private const decimal DollarToPesoExchangeRate = 60.00m;
    private const decimal InterestRate = 0.02m; //2% Interest Rate Charge

    public AccountType AccountType => AccountType.Dollar;
    public MoneyType MoneyType => moneyType;

    public new void Deposit(decimal amount)
    {
        if (MoneyType == MoneyType.Dollar)
            amount = GetAmountLessInterest(amount);

        Balance += amount;
    }

    private decimal ConvertDollarToPeso(decimal pesoAmount) => pesoAmount * DollarToPesoExchangeRate;
    private decimal GetAmountLessInterest(decimal amount) => (1 - InterestRate) * ConvertDollarToPeso(amount);
}