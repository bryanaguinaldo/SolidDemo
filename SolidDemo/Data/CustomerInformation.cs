using SolidDemo.BankAccounts.Accounts;
using SolidDemo.BankAccounts.Interfaces;
using SolidDemo.LoanAccounts.Interfaces;

namespace SolidDemo.Data;

public class UserInformation
{
    public required int customerId { get; set; }
    public required string FullName { get; set; }
    public required string Password { get; set; }
    public required List<IAccount> AccountList { get; set; }
    public required List<ILoan> LoanList { get; set; }
}

public static class CustomerData
{
    public static List<UserInformation> Information { get; } = new List<UserInformation>
    {
        new UserInformation
        {
            customerId = 1001,
            FullName = "Bryan Joseph Aguinaldo",
            Password = "CPQ",
            AccountList = new List<IAccount>
            {
                new SavingsAccount(1001, 50000.00m),
                new CurrentAccount(1002, 50000.00m, 5000m),
                new TimeDepositAccount(1003, 50000m, DateTime.Today.Subtract(TimeSpan.FromDays(29)), 30),
                new DollarAccount(1004, 50000.00m)
            },
            LoanList = new List<ILoan>()
        },
        new UserInformation
        {
            customerId = 1002,
            FullName = "Hansel Avellana",
            Password = "CPQ",
            AccountList = new List<IAccount>
            {
                new SavingsAccount(1001, 50000.00m),
                new CurrentAccount(1002, 50000.00m, 5000m),
                new TimeDepositAccount(1003, 50000m, DateTime.Today.Subtract(TimeSpan.FromDays(29)), 30),
                new DollarAccount(1004, 50000.00m)
            },
            LoanList = new List<ILoan>()
        },
        new UserInformation
        {
            customerId = 1003,
            FullName = "Jeanson Avenilla",
            Password = "CPQ",
            AccountList = new List<IAccount>
            {
                new SavingsAccount(1001, 50000.00m),
                new CurrentAccount(1002, 50000.00m, 5000m),
                new TimeDepositAccount(1003, 50000m, DateTime.Today.Subtract(TimeSpan.FromDays(29)), 30),
                new DollarAccount(1004, 50000.00m)
            },
            LoanList = new List<ILoan>()
        }
    };
}