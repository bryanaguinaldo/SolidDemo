using Microsoft.Extensions.DependencyInjection;
using SolidDemo.BankAccounts.Enums;
using SolidDemo.BankAccounts.Interfaces;
using SolidDemo.Enums;
using SolidDemo.Interfaces;
using SolidDemo.LoanAccounts.Enums;
using SolidDemo.LoanAccounts.Interfaces;
using SolidDemo.LoanAccounts.Loans;

namespace SolidDemo;

public class SolidDemo : BaseDemo
{
    private readonly ILoginService _loginService;
    private readonly IBankService _bankService;
    private readonly ILoanService _loanService;
    private readonly ILoggingService _loggingService;

    public SolidDemo()
    {
        _loginService = ServiceProvider.GetRequiredService<ILoginService>();
        _bankService = ServiceProvider.GetRequiredService<IBankService>();
        _loanService = ServiceProvider.GetRequiredService<ILoanService>();
        _loggingService = ServiceProvider.GetRequiredService<ILoggingService>();
    }
    
    private Customer customer;
    private const int MaxAttempts = 3;

    public void Start()
    {
        _loggingService.LogMessage("\nWelcome to CPQ Manila Bank\n");

        Login(out var customerId);

        var userInformation = _loginService.GetUserInformation(customerId);
        customer = new Customer(customerId, userInformation.FullName, userInformation.AccountList, userInformation.LoanList);

        _loggingService.LogMessage($"\nWelcome, {userInformation.FullName}!");

        ServiceOptions();

        Console.ReadLine();
    }

    private void Login(out int customerId)
    {
        customerId = GetValidAccountId()!;

        if (customerId == 0)
            ExitWithMessage("Too many incorrect attempts. Exiting...");

        bool isValid = PerformPasswordValidation(customerId!);

        if (!isValid)
            ExitWithMessage("Too many incorrect attempts. Exiting...");
    }

    private int GetValidAccountId()
    {
        for (int attempts = 0; attempts < MaxAttempts; attempts++)
        {
            var customerId = _loggingService.GetInput("\nEnter your customer ID to proceed: ");
            int.TryParse(customerId, out var input);

            if (_loginService.IsCustomerIdValid(input))
                return input;

            _loggingService.LogMessage($"No account ID ({input}) found. Please enter again.");
        }

        return 0;
    }

    private bool PerformPasswordValidation(int customerId)
    {
        for (int attempts = 0; attempts < MaxAttempts; attempts++)
        {
            var password = _loggingService.GetInput("\nEnter your password: ");

            if (_loginService.IsPasswordValid(customerId, password!))
                return true;

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

        while (true)
        {
            var serviceOption = _loggingService.GetInput("Choose: ");

            if (serviceOption == "1")
                PerformBankAccountOperations();
            else if (serviceOption == "2")
                PerformLoanAccountOperations();
            else
                Console.Write("Invalid option. ");
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

        while (true)
        {
            var choose = _loggingService.GetInput("Choose: ");
            int.TryParse(choose, out var input);

            if (input > 0 && input < 5)
            {
                accountId = customer.Accounts.Where(s => (int)s.AccountType == (input - 1)).Select(s => s.AccountId).FirstOrDefault();
                break;
            }

            Console.Write("Invalid option. ");
        }

        _loggingService.LogMessage();
        _loggingService.LogMessage("1. Deposit");
        _loggingService.LogMessage("2. Withdraw");
        _loggingService.LogMessage();
        Console.Write("Choose: ");

        while (true)
        {
            var choose = _loggingService.GetInput("Choose: ");
            int.TryParse(choose, out var input);

            if (input > 0 && input < 3)
            {
                bankServiceAction = (BankServiceType)(input - 1);
                break;
            }

            Console.Write("Invalid option. ");
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

            while (true)
            {
                var choose = _loggingService.GetInput("Choose: ");
                int.TryParse(choose, out var input);

                if (input > 0 && input < 3)
                {
                    if (account is IDollarAccount dollarAccount)
                        dollarAccount.MoneyType = (MoneyType)(input - 1);

                    break;
                }

                Console.Write("Invalid option. ");
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

        while (true)
        {
            var choose = _loggingService.GetInput("Choose: ");
            int.TryParse(choose, out var input);

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
                Console.Write("Invalid option. ");
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
            _loanService.AddLoan(customer, loan);
        else
            _loggingService.LogMessage("Cancelling loan request...");

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