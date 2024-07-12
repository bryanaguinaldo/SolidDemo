using SolidDemo.Enums;
using SolidDemo.Interfaces;

namespace SolidDemo.Validations;

internal class DollarAccountValidation : IAccountValidation
{
    public AccountType AccountType => AccountType.Current;

    public bool IsValid(IAccount account, decimal amount)
    {
        if (account is IDollarAccount dollarAccount)
        {
            return amount <= 0 || amount > account.Balance ? false : true;
        }

        throw new ArgumentException($"Account type {account} is not Dollar Account");
    }
}
