using SolidDemo.Accounts;

namespace SolidDemo.Validations;
internal interface IAccountValidation
{
    AccountType AccountType { get; }

    bool IsValid(IAccount account, decimal amount);
}