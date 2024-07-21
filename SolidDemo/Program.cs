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
        Console.WriteLine("");
        Console.WriteLine("Welcome to CPQ Manila Bank");
        Console.WriteLine("");

        Login(out var customerFullName, out var customerId);

        Console.WriteLine("");
        Console.WriteLine($"Welcome, {customerFullName}!");
        Console.WriteLine("");

        ServiceOptions(customerFullName, customerId);

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
        // Need to clean this up
        InitializeBankAccounts(out var serviceCollection);

        var savingsAccount = new SavingsAccount(1001, 50000.00m);
        var currentAccount = new CurrentAccount(1002, 50000.00m, 5000m);
        var timeDepositAccount = new TimeDepositAccount(1003, 50000m, DateTime.Today.Subtract(TimeSpan.FromDays(29)), 30);
        var dollarAccount = new DollarAccount(1004, 50000.00m, MoneyType.Dollar);

        int id = int.Parse(customerId);

        var serviceProvider = serviceCollection.BuildServiceProvider();

        var accountList = new List<IAccount>
        {
            savingsAccount,
            currentAccount,
            timeDepositAccount,
            dollarAccount
        };

        var bankService = serviceProvider.GetRequiredService<IBankService>();

        var customer = new Customer(id, customerFullName, accountList);

        string? accountOption;
        string? actionOption;
        int account;
        int action;

        Console.WriteLine("1. Savings Account");
        Console.WriteLine("2. Current Account");
        Console.WriteLine("3. Time Deposit Account");
        Console.WriteLine("4. Dollar Account");
        Console.Write("Choose: ");

        while (true)
        {
            accountOption = Console.ReadLine();
            var result = int.TryParse(accountOption, out account);
            if (result && account >= 1 && account <= 4) break;
            else Console.Write("Invalid option. Choose again: ");
        }

        Console.WriteLine("1. Withdraw");
        Console.WriteLine("2. Deposit");
        Console.Write("Choose: ");

        while (true)
        {
            actionOption = Console.ReadLine();
            var result = int.TryParse(actionOption, out action);
            if (result && action >= 1 && action <= 2) break; 
            else Console.Write("Invalid option. Choose again: ");
        }

        Console.Write("Amount: ");

        decimal amount;

        while (!decimal.TryParse(Console.ReadLine(), out amount)) {
            Console.Write("Invalid amount. Enter again: ");
        }

        // Putting this here for readability before clean up
        var actionType = actionOption == "1" ? "Withdraw" : "Deposit";

        if (actionType == "Withdraw") bankService.Withdraw(customer, accountList[account - 1].AccountId, amount);
        else bankService.Deposit(customer, accountList[account - 1].AccountId, amount);

        Console.WriteLine("Thank you for using CPQ Manila Bank!");
        Console.ReadLine();

        Environment.Exit(0);
    }

    static void PerformLoanAccountOperations()
    {

    }

    static void InitializeBankAccounts(out ServiceCollection serviceCollection)
    {
        serviceCollection = new ServiceCollection();

        serviceCollection.AddScoped<ILoggingService, LoggingService>();
        serviceCollection.AddScoped<IBankService, BankService>();
        serviceCollection.AddScoped<IAccountValidation, SavingsAccountValidation>();
        serviceCollection.AddScoped<IAccountValidation, CurrentAccountValidation>();
        serviceCollection.AddScoped<IAccountValidation, TimeDepositValidation>();
        serviceCollection.AddScoped<IAccountValidation, DollarAccountValidation>();
    }
}
