using SolidDemo.BankAccounts.Enums;

namespace SolidDemo.BankAccounts.Interfaces;

internal interface IDollarAccount : IAccount
{
    MoneyType MoneyType { get; set; }
}