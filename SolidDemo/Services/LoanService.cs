using SolidDemo.Interfaces;

namespace SolidDemo.Services;

public class LoanService : ILoanService
{
    private readonly ILoggingService _loggingService;

    public LoanService(ILoggingService loggingService)
    {
        _loggingService = loggingService;
    }

    public void DisplayLoanDetails(Customer customer)
    {
        var loanList = customer.Loans;

        _loggingService.LogMessage("");
        _loggingService.LogMessage($"Loan details as of {DateTime.Now:MM/dd/yyyy}");
        _loggingService.LogMessage("");
        
        if (customer.Loans.Any())
        {
            _loggingService.LogMessage("LoanId      Loan Type        Total Amount        Monthly Payment     Duration");

            foreach (var loan in loanList)
            {
                var loadId = loan.LoanId.ToString();
                var loanType = loan.LoanType.ToString();
                var totalAmount = loan.TotalAmount().ToString();
                var monthlyPayment = loan.MonthlyAmortization().ToString();
                var duration = loan.Duration.ToString();

                _loggingService.LogMessage($"{loadId.PadRight(12)}{loanType.PadRight(17)}{($"P{totalAmount.PadRight(20)}")}{($"P{monthlyPayment}").PadRight(20)}{duration} mo");
            }

            _loggingService.LogMessage("");
            _loggingService.LogMessage($"Total loan amount: P{loanList.Select(s => s.TotalAmount()).Sum()}");
            _loggingService.LogMessage("");
        }
        else
        {
            _loggingService.LogMessage("No loan to display");
            _loggingService.LogMessage("");
        }
    }
}