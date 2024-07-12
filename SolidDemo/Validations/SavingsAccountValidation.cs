using SolidDemo.Accounts;

namespace SolidDemo.Validations;

internal class SavingsAccountValidation : IAccountValidation
{
    public AccountType AccountType => AccountType.Savings;

    public bool IsValid(IAccount account, decimal amount)
    {
        return amount <= 0 || amount > account.Balance ? false : true;
    }
}
