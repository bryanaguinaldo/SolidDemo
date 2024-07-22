using SolidDemo.BankAccounts.Accounts;
using SolidDemo.BankAccounts.Interfaces;
using SolidDemo.LoanAccounts.Interfaces;

namespace SolidDemo.Data;

public class UserInformation
{
    public required string FullName { get; set; }
    public required string Password { get; set; }
    public required List<IAccount> AccountList { get; set; }
    public required List<ILoan> LoanList { get; set; }
}

public static class CustomerData
{
    public static Dictionary<int, UserInformation> Information { get; } = new Dictionary<int, UserInformation>
    {
        { 
            1001, 
            new UserInformation 
            {
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
            } 
        },
        {
            1002,
            new UserInformation 
            { 
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
            }
        },
        {
            1003,
            new UserInformation
            {
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
        }
    };
}