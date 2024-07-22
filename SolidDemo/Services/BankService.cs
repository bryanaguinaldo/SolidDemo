using SolidDemo.BankAccounts.Enums;
using SolidDemo.BankAccounts.Interfaces;
using SolidDemo.Interfaces;

namespace SolidDemo.Services;

internal class BankService : IBankService
{
    private readonly ILoggingService _loggingService;
    private readonly IDictionary<AccountType, IAccountValidation> _accountValidations;

    public BankService(ILoggingService loggingService, IEnumerable<IAccountValidation> accountValidations)
    {
        _loggingService = loggingService;
        _accountValidations = accountValidations.ToDictionary(x => x.AccountType, y => y);
    }

    public void Deposit(Customer customer, int accountId, decimal amount)
    {
        var account = customer.GetBankAccount(accountId);

        if (!_accountValidations.TryGetValue(account.AccountType, out var accountValidation))
            throw new ArgumentException($"Account type {account} is not Valid");

        if (accountValidation.IsValid(account, amount))
        {
            account.Deposit(amount);
            _loggingService.LogMessage($"Deposit of {amount} successful. New balance: {Math.Round(account.Balance)}");
        }
        else
        {
            if (account is ITimeDepositAccount timeDeposit)
                LogTimeDepositError(timeDeposit);
            else
                _loggingService.LogMessage("Withdrawal failed. Check the amount and balance.");
        }
    }

    public void Withdraw(Customer customer, int accountId, decimal amount)
    {
        var account = customer.GetBankAccount(accountId);

        if (!_accountValidations.TryGetValue(account.AccountType, out var accountValidation))
            throw new ArgumentException($"Account type {account} is not Valid");

        if (accountValidation.IsValid(account, amount))
        {
            account.Withdraw(amount);
            _loggingService.LogMessage($"Withdrawal of {amount} successful. New balance: {Math.Round(account.Balance)}");
        }
        else
        {
            if (account is ITimeDepositAccount timeDeposit)
                LogTimeDepositError(timeDeposit);
            else
                _loggingService.LogMessage("Withdrawal failed. Check the amount and balance.");
        }
    }

    private void LogTimeDepositError(ITimeDepositAccount timeDeposit)
    {
        if (!timeDeposit.IsMatured())
            _loggingService.LogMessage("Time Deposit account did not reach maturity date");
    }
}
