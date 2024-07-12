using SolidDemo.Enums;
using SolidDemo.Interfaces;

namespace SolidDemo.Accounts;

internal class DollarAccount(int accountId, decimal balance, MoneyType moneyType) : Account(accountId, balance), IDollarAccount
{
    private const decimal DollarExchangeRateToPeso = 60.00M;
    public AccountType AccountType => AccountType.Dollar;
    public MoneyType MoneyType => moneyType;
    public new void Deposit(decimal amount)
    {
        if (MoneyType == MoneyType.Dollar)
            amount = (decimal)0.98 * ConvertDollarToPeso(amount);

        Balance += amount;
    }
    private decimal ConvertDollarToPeso(decimal pesoAmount)
    {
        return pesoAmount * DollarExchangeRateToPeso;
    }
}