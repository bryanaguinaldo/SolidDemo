using SolidDemo.Accounts;

namespace SolidDemo.Validations;
internal class CurrentAccountValidation : IAccountValidation
{
    public AccountType AccountType => AccountType.Current;

    public bool IsValid(IAccount account, decimal amount)
    {
        if (account is ICurrentAccount currentAccount)
        {
            var allowedAmountToWithdraw = currentAccount.OverDraft;

            if (amount <= 0 || amount > allowedAmountToWithdraw)
                return false;

            return true;
        }

        throw new ArgumentException($"Account type {account} is not Current Account");
    }
}
