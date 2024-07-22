using Microsoft.Extensions.DependencyInjection;
using SolidDemo.BankAccounts.Enums;
using SolidDemo.BankAccounts.Interfaces;
using SolidDemo.Data;
using SolidDemo.Enums;
using SolidDemo.Interfaces;

namespace SolidDemo;

public class SolidDemo : BaseDemo
{
    private readonly IBankService _bankService;
    private readonly ILoanService _loanService;

    public SolidDemo()
    {
        _bankService = ServiceProvider.GetRequiredService<IBankService>();
        _loanService = ServiceProvider.GetRequiredService<ILoanService>();
    }
    
    private Customer customer;
    private const int MaxAttempts = 3;

    public void Start()
    {
        Console.WriteLine("\nWelcome to CPQ Manila Bank\n");

        Login(out var id, out var userInformation);

        customer = new Customer(id, userInformation.FullName, userInformation.AccountList, userInformation.LoanList);

        Console.WriteLine($"\nWelcome, {userInformation.FullName}!");

        ServiceOptions();

        Console.ReadLine();
    }

    private void Login(out int customerId, out UserInformation userInformation)
    {
        customerId = GetValidAccountId()!;

        if (customerId == 0)
            ExitWithMessage("Too many incorrect attempts. Exiting...");

        bool isValid = PerformPasswordValidation(customerId!);

        if (!isValid)
            ExitWithMessage("Too many incorrect attempts. Exiting...");

        userInformation = CustomerData.Information[customerId!];
    }

    private int GetValidAccountId()
    {
        for (int attempts = 0; attempts < MaxAttempts; attempts++)
        {
            Console.Write("\nEnter your account ID to proceed: ");
            int.TryParse(Console.ReadLine()!, out var input);

            if (CustomerData.Information.ContainsKey(input))
                return input;

            Console.WriteLine($"No account ID ({input}) found. Please enter again.");
        }

        return 0;
    }

    private bool PerformPasswordValidation(int customerId)
    {
        for (int attempts = 0; attempts < MaxAttempts; attempts++)
        {
            Console.Write("\nEnter your password: ");
            string password = Console.ReadLine()!;

            if (CustomerData.Information[customerId].Password == password)
            {
                return true;
            }

            Console.WriteLine("Wrong password. Please try again.");
        }

        return false;
    }

    private void ServiceOptions()
    {
        Console.WriteLine();
        Console.WriteLine("1. Bank Account Services");
        Console.WriteLine("2. Loan Account Services");
        Console.WriteLine();
        Console.Write("Choose: ");

        while (true)
        {
            var serviceOption = Console.ReadLine();

            if (serviceOption == "1")
                PerformBankAccountOperations();
            else if (serviceOption == "2")
                PerformLoanAccountOperations();
            else
                Console.Write("Invalid option. Choose again: ");
        }
    }

    private void PerformBankAccountOperations()
    {
        int accountId = 0;
        BankServiceType bankServiceAction = 0;
        decimal amount = 0;

        Console.WriteLine();
        Console.WriteLine("1. Savings Account");
        Console.WriteLine("2. Current Account");
        Console.WriteLine("3. Time Deposit Account");
        Console.WriteLine("4. Dollar Account");
        Console.WriteLine();
        Console.Write("Choose: ");

        while (true)
        {
            int.TryParse(Console.ReadLine(), out var input);

            if (input > 0 && input < 5)
            {
                accountId = customer.Accounts.Where(s => (int)s.AccountType == (input - 1)).Select(s => s.AccountId).FirstOrDefault();
                break;
            }
            else
            {
                Console.Write("Invalid option. Choose again: ");
            }
        }

        Console.WriteLine();
        Console.WriteLine("1. Deposit");
        Console.WriteLine("2. Withdraw");
        Console.WriteLine();
        Console.Write("Choose: ");

        while (true)
        {
            int.TryParse(Console.ReadLine(), out var input);

            if (input > 0 && input < 3)
            {
                bankServiceAction = (BankServiceType)(input - 1);
                break;
            }
            else
            {
                Console.Write("Invalid option. Choose again: ");
            }
        }

        Console.Write("\nAmount: ");

        while (!decimal.TryParse(Console.ReadLine(), out amount))
        {
            Console.Write("Invalid amount. Enter again: ");
        }

        var account = customer.GetBankAccount(accountId);

        if (account.AccountType is AccountType.Dollar)
        {
            Console.WriteLine();
            Console.WriteLine("1. Money (dollar)");
            Console.WriteLine("2. Money (peso)");
            Console.WriteLine();
            Console.Write("Choose: ");

            while (true)
            {
                int.TryParse(Console.ReadLine(), out var input);

                if (input > 0 && input < 3)
                {
                    if (account is IDollarAccount dollarAccount)
                        dollarAccount.MoneyType = (MoneyType)(input - 1);

                    break;
                }
                else
                {
                    Console.Write("Invalid option. Choose again: ");
                }
            }
        }

        Console.WriteLine();

        if (bankServiceAction == BankServiceType.Deposit)
            _bankService.Deposit(customer, accountId, amount);
        else if (bankServiceAction == BankServiceType.Widthdraw)
            _bankService.Withdraw(customer, accountId, amount);

        Console.WriteLine();
        Console.Write("Do you want to transact again? (Y or N): ");
        var needTransaction = Console.ReadLine();

        if (needTransaction is "Y" or "y")
            ServiceOptions();
        else
            ExitWithMessage("Thank you for using CPQ Manila Bank!");
    }

    private void PerformLoanAccountOperations()
    {
        Console.WriteLine();
        Console.WriteLine("1. Personal Loan");
        Console.WriteLine("2. Car Loan");
        Console.WriteLine("3. Home Loan");
        Console.WriteLine();
        Console.Write("Choose: ");
    }

    private void ExitWithMessage(string message)
    {
        Console.WriteLine(message);
        Console.ReadLine();

        Environment.Exit(0);
    }
}