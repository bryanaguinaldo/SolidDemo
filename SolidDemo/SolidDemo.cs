using Microsoft.Extensions.DependencyInjection;
using SolidDemo.BankAccounts.Enums;
using SolidDemo.BankAccounts.Interfaces;
using SolidDemo.Data;
using SolidDemo.Enums;
using SolidDemo.Interfaces;
using SolidDemo.LoanAccounts.Enums;
using SolidDemo.LoanAccounts.Interfaces;
using SolidDemo.LoanAccounts.Loans;

namespace SolidDemo;

public class SolidDemo : BaseDemo
{
    private readonly IBankService _bankService;
    private readonly ILoanService _loanService;
    private readonly ILoggingService _loggingService;

    public SolidDemo()
    {
        _bankService = ServiceProvider.GetRequiredService<IBankService>();
        _loanService = ServiceProvider.GetRequiredService<ILoanService>();
        _loggingService = ServiceProvider.GetRequiredService<ILoggingService>();
    }
    
    private Customer customer;
    private const int MaxAttempts = 3;

    public void Start()
    {
        _loggingService.LogMessage("\nWelcome to CPQ Manila Bank\n");

        Login(out var id, out var userInformation);

        customer = new Customer(id, userInformation.FullName, userInformation.AccountList, userInformation.LoanList);

        _loggingService.LogMessage($"\nWelcome, {userInformation.FullName}!");

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

            _loggingService.LogMessage($"No account ID ({input}) found. Please enter again.");
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

            _loggingService.LogMessage("Wrong password. Please try again.");
        }

        return false;
    }

    private void ServiceOptions()
    {
        _loggingService.LogMessage();
        _loggingService.LogMessage("1. Bank Account Services");
        _loggingService.LogMessage("2. Loan Account Services");
        _loggingService.LogMessage();
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

        _loggingService.LogMessage();
        _loggingService.LogMessage("1. Savings Account");
        _loggingService.LogMessage("2. Current Account");
        _loggingService.LogMessage("3. Time Deposit Account");
        _loggingService.LogMessage("4. Dollar Account");
        _loggingService.LogMessage();
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

        _loggingService.LogMessage();
        _loggingService.LogMessage("1. Deposit");
        _loggingService.LogMessage("2. Withdraw");
        _loggingService.LogMessage();
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

        Console.Write("\nAmount: P");

        while (!decimal.TryParse(Console.ReadLine(), out amount))
        {
            Console.Write("Invalid amount. Enter again: P");
        }

        var account = customer.GetBankAccount(accountId);

        if (account.AccountType is AccountType.Dollar)
        {
            _loggingService.LogMessage();
            _loggingService.LogMessage("1. Money (dollar)");
            _loggingService.LogMessage("2. Money (peso)");
            _loggingService.LogMessage();
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

        _loggingService.LogMessage();

        if (bankServiceAction == BankServiceType.Deposit)
            _bankService.Deposit(customer, accountId, amount);
        else if (bankServiceAction == BankServiceType.Withdraw)
            _bankService.Withdraw(customer, accountId, amount);

        _loggingService.LogMessage();
        var needTransaction = _loggingService.GetInput("Do you want to transact again? (Y or N): ");

        if (needTransaction is "Y" or "y")
            ServiceOptions();
        else
            ExitWithMessage("Thank you for using CPQ Manila Bank!");
    }

    private void PerformLoanAccountOperations()
    {
        ILoan loan = null;
        LoanType loanOption = 0;
        decimal amount = 0;
        decimal duration = 0;

        var loanId = new Random().Next(5000, 9999);

        _loggingService.LogMessage();
        _loggingService.LogMessage("1. Personal Loan");
        _loggingService.LogMessage("2. Car Loan");
        _loggingService.LogMessage("3. Home Loan");
        _loggingService.LogMessage("4. Display all Loan");
        _loggingService.LogMessage();
        Console.Write("Choose: ");

        while (true)
        {
            int.TryParse(Console.ReadLine(), out var input);

            if (input > 0 && input < 5)
            {
                if (input == 4)
                {
                    _loanService.DisplayLoanDetails(customer);
                    PerformLoanAccountOperations();
                }
                else
                {
                    loanOption = (LoanType)(input - 1);
                }

                break;
            }
            else
            {
                Console.Write("Invalid option. Choose again: ");
            }
        }

        Console.Write("\nAmount: P");

        while (!decimal.TryParse(Console.ReadLine(), out amount))
        {
            Console.Write("Invalid amount. Enter again: P");
        }

        Console.Write("\nDuration (years): ");

        while (!decimal.TryParse(Console.ReadLine(), out duration))
        {
            Console.Write("Invalid amount. Enter again: ");
        }

        if (loanOption == LoanType.PersonalLoan)
            loan = new PersonalLoan(loanId, amount, duration);
        if (loanOption == LoanType.CarLoan)
            loan = new CarLoan(loanId, amount, duration);
        if (loanOption == LoanType.HomeLoan)
            loan = new HomeLoan(loanId, amount, duration);

        loan.OutputMessage();

        _loggingService.LogMessage();
        var proceed = _loggingService.GetInput("Proceed to loan (Y or N): ");

        if (proceed is "Y" or "y")
        {
            _loanService.AddLoan(customer, loan);
            _loanService.DisplayLoanDetails(customer);
            _loggingService.LogMessage($"Successfully creating {loanOption.ToString()} loan account.");
        }
        else
        {
            _loggingService.LogMessage("Cancelling loan request...");
        }

        _loggingService.LogMessage();
        var needTransaction = _loggingService.GetInput("Do you want to transact again? (Y or N): ");

        if (needTransaction is "Y" or "y")
            ServiceOptions();
        else
            ExitWithMessage("Thank you for using CPQ Manila Bank!");
    }

    private void ExitWithMessage(string message)
    {
        _loggingService.LogMessage(message);
        Console.ReadLine();

        Environment.Exit(0);
    }
}