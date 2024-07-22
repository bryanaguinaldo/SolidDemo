using SolidDemo.BankAccounts.Enums;

namespace SolidDemo.BankAccounts.Interfaces;

public interface IDollarAccount : IAccount
{
    MoneyType MoneyType { get; set; }
}