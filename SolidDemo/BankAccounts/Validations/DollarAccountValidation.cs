using SolidDemo.BankAccounts.Enums;
using SolidDemo.BankAccounts.Interfaces;

namespace SolidDemo.BankAccounts.Validations;

public class DollarAccountValidation : IAccountValidation
{
    public AccountType AccountType => AccountType.Dollar;

    public bool IsValid(IAccount account, decimal amount)
    {
        if (account is IDollarAccount dollarAccount)
            return amount <= 0 || amount > account.Balance ? false : true;

        throw new ArgumentException($"Account type {account} is not Dollar Account");
    }
}
