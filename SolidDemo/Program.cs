using Microsoft.Extensions.DependencyInjection;
using SolidDemo.BankAccounts.Accounts;
using SolidDemo.BankAccounts.Enums;
using SolidDemo.BankAccounts.Interfaces;
using SolidDemo.BankAccounts.Validations;
using SolidDemo.Data;
using SolidDemo.Services;
using System.Runtime.CompilerServices;

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

        Login(out var customerFullName, out var customerId);

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

        ServiceOptions(customerFullName, customerId);

        bankService.Withdraw(customer, 1001, 100.00m);
        bankService.Withdraw(customer, 1002, 600.00m);
        bankService.Withdraw(customer, 1003, 300m);
        bankService.Withdraw(customer, 1004, 200m);

        Console.ReadLine();
    }

    static void Login(out string fullName, out string customerId)
    {
        customerId = GetValidAccountId()!;

        if (customerId == null)
        {
            Console.WriteLine("Too many incorrect attempts. Exiting...");
            Console.ReadLine();

            Environment.Exit(0);
        }

        bool isValid = PerformPasswordValidation(customerId);

        if (!isValid)
        {
            Console.WriteLine("Too many incorrect attempts. Exiting...");
            Console.ReadLine();

            Environment.Exit(0);
        }

        fullName = CustomerData.Information[customerId].FullName!;
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

    static bool PerformPasswordValidation(string customerId)
    {
        int attempts = 0;

        while (attempts < 3)
        {
            Console.Write("\nEnter your password: ");
            string password = Console.ReadLine()!;

            if (CustomerData.Information[customerId].Password == password)
                return true;

            attempts++;

            Console.WriteLine("Wrong password. Please try again.");
        }

        return false;
    }

    static void ServiceOptions(string customerFullName, string customerId)
    {
        Console.WriteLine("1. Bank Account Services");
        Console.WriteLine("2. Loan Account Services");
        Console.Write("Choose: ");

        while (true)
        {
            var serviceOption = Console.ReadLine();

            if (serviceOption == "1")
                PerformBankAccountOperations(customerFullName, customerId);
            else if (serviceOption == "2")
                PerformLoanAccountOperations();
            else
                Console.Write("Invalid option. Choose again: ");
        }
    }

    static void PerformBankAccountOperations(string customerFullName, string customerId)
    {
        int id = int.Parse(customerId);
        var savingsAccount = new SavingsAccount(1001, 500.00m);
        var currentAccount = new CurrentAccount(1002, 500.00m, 100m);

        var serviceCollection = new ServiceCollection();

        serviceCollection.AddScoped<ILoggingService, LoggingService>();
        serviceCollection.AddScoped<IBankService, BankService>();
        serviceCollection.AddScoped<IAccountValidation, SavingsAccountValidation>();
        serviceCollection.AddScoped<IAccountValidation, CurrentAccountValidation>();

        var serviceProvider = serviceCollection.BuildServiceProvider();

        var bankService = serviceProvider.GetRequiredService<IBankService>();

        var customer = new Customer(id, customerFullName, new List<IAccount>
        {
            savingsAccount,
            currentAccount,
        });

        string? accountOption;
        string? actionOption;

        Console.WriteLine("1. Savings Account");
        Console.WriteLine("2. Current Account");
        Console.Write("Choose: ");

        while (true)
        {
            accountOption = Console.ReadLine();
            if (accountOption == "1" || accountOption == "2") break;
            else Console.Write("Invalid option. Choose again: ");
        }

        Console.WriteLine("1. Withdraw");
        Console.WriteLine("2. Deposit");
        Console.Write("Choose: ");

        while (true)
        {
            actionOption = Console.ReadLine();
            if (actionOption == "1" || actionOption == "2") break;
            else Console.Write("Invalid option. Choose again: ");
        }

        Console.Write("Amount: ");

        decimal amount;

        while (!decimal.TryParse(Console.ReadLine(), out amount)) {
            Console.Write("Invalid amount. Enter again: ");
        }

        // Putting this here for readability before clean up
        var accountType = accountOption == "1" ? AccountType.Savings : AccountType.Current;
        var actionType = actionOption == "1" ? "Withdraw" : "Deposit";

        if (accountType == AccountType.Savings)
        {
            if (actionType == "Withdraw") bankService.Withdraw(customer, savingsAccount.AccountId, amount);
            else bankService.Deposit(customer, savingsAccount.AccountId, amount);
        }
        else
        {
            if (actionType == "Withdraw") bankService.Withdraw(customer, currentAccount.AccountId, amount);
            else bankService.Deposit(customer, currentAccount.AccountId, amount);
        }

        Console.WriteLine("Thank you for using CPQ Manila Bank!");
        Console.ReadLine();

        Environment.Exit(0);
    }

    static void PerformLoanAccountOperations()
    {

    }

    static void InitializeBankAccounts()
    {

    }
}
