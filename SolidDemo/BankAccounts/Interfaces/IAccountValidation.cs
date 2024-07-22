using SolidDemo.BankAccounts.Enums;

namespace SolidDemo.BankAccounts.Interfaces;
public interface IAccountValidation
{
    AccountType AccountType { get; }

    bool IsValid(IAccount account, decimal amount);
}