using SolidDemo.Enums;

namespace SolidDemo.Interfaces;
internal interface IAccountValidation
{
    AccountType AccountType { get; }

    bool IsValid(IAccount account, decimal amount);
}