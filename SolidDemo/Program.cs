using Microsoft.Extensions.DependencyInjection;
using SolidDemo.BankAccounts.Accounts;
using SolidDemo.BankAccounts.Enums;
using SolidDemo.BankAccounts.Interfaces;
using SolidDemo.BankAccounts.Validations;
using SolidDemo.Data;
using SolidDemo.Services;

namespace SolidDemo;

internal class Program
{
    static void Main(string[] args)
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection.AddScoped<ILoggingService, LoggingService>();
        serviceCollection.AddScoped<IBankService, BankService>();  
        serviceCollection.AddScoped<IAccountValidation, SavingsAccountValidation>();
        serviceCollection.AddScoped<IAccountValidation, CurrentAccountValidation>();
        serviceCollection.AddScoped<IAccountValidation, TimeDepositValidation>();
        serviceCollection.AddScoped<IAccountValidation, DollarAccountValidation>();

        var serviceProvider = serviceCollection.BuildServiceProvider();

        var bankService = serviceProvider.GetRequiredService<IBankService>();

        Console.WriteLine("");
        Console.WriteLine("Welcome to CPQ Manila Bank");
        Console.WriteLine("");

        Login(out var customerFullName);

        Console.WriteLine("");
        Console.WriteLine($"Welcome, {customerFullName}!");
        Console.WriteLine("");

        var savingsAccount = new SavingsAccount(1001, 500.00m);
        var currentAccount = new CurrentAccount(1002, 500.00m, 100m);
        var timeDepositAccount = new TimeDepositAccount(1003, 500m, DateTime.Today.Subtract(TimeSpan.FromDays(29)), 30);
        var dollarAccount = new DollarAccount(1004, 500.00m, MoneyType.Dollar);

        var customer = new Customer(1, customerFullName, new List<IAccount>
        {
            savingsAccount,
            currentAccount,
            timeDepositAccount,
            dollarAccount
        });

        ServiceOptions();

        bankService.Withdraw(customer, 1001, 100.00m);
        bankService.Withdraw(customer, 1002, 600.00m);
        bankService.Withdraw(customer, 1003, 300m);
        bankService.Withdraw(customer, 1004, 200m);

        Console.ReadLine();
    }

    static void Login(out string fullName)
    {
        string accountId = GetValidAccountId()!;

        if (accountId == null)
        {
            Console.WriteLine("Too many incorrect attempts. Exiting...");
            Console.ReadLine();

            Environment.Exit(0);
        }

        bool isValid = PerformPasswordValidation(accountId);

        if (!isValid)
        {
            Console.WriteLine("Too many incorrect attempts. Exiting...");
            Console.ReadLine();

            Environment.Exit(0);
        }

        fullName = CustomerData.Information[accountId].FullName!;
    }

    static string? GetValidAccountId()
    {
        int attempts = 0;

        while (attempts < 3)
        {
            Console.Write("\nEnter your account ID to proceed: ");
            string input = Console.ReadLine()!;

            if (CustomerData.Information.ContainsKey(input))
                return input;

            attempts++;

            Console.WriteLine($"No account ID ({input}) found. Please enter again.");
        }

        return null;
    }

    static bool PerformPasswordValidation(string accountId)
    {
        int attempts = 0;

        while (attempts < 3)
        {
            Console.Write("\nEnter your password: ");
            string password = Console.ReadLine()!;

            if (CustomerData.Information[accountId].Password == password)
                return true;

            attempts++;

            Console.WriteLine("Wrong password. Please try again.");
        }

        return false;
    }

    static void ServiceOptions()
    {
        Console.WriteLine("1. Bank Account Services");
        Console.WriteLine("2. Loan Account Services");
        Console.Write("Choose: ");

        while (true)
        {
            string serviceOption = Console.ReadLine();

            if (serviceOption == "1")
                PerformBankAccountOperations();
            else if (serviceOption == "2")
                PerformLoanAccountOperations();
            else
                Console.Write("Invalid option. Choose again: ");
        }
    }

    static void PerformBankAccountOperations()
    {

    }

    static void PerformLoanAccountOperations()
    {

    }

    static void InitializeBankAccounts()
    {

    }
}
