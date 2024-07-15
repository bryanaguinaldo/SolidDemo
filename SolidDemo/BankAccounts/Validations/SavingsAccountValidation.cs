using SolidDemo.BankAccounts.Enums;
using SolidDemo.BankAccounts.Interfaces;

namespace SolidDemo.BankAccounts.Validations;

internal class SavingsAccountValidation : IAccountValidation
{
    public AccountType AccountType => AccountType.Savings;

    public bool IsValid(IAccount account, decimal amount)
    {
        return amount <= 0 || amount > account.Balance ? false : true;
    }
}
