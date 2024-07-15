using SolidDemo.BankAccounts.Enums;

namespace SolidDemo.BankAccounts.Interfaces;
internal interface IAccountValidation
{
    AccountType AccountType { get; }

    bool IsValid(IAccount account, decimal amount);
}