using SolidDemo.Interfaces;

namespace SolidDemo.Services;

public class LoanService : ILoanService
{
    private readonly ILoggingService _loggingService;

    public LoanService(ILoggingService loggingService)
    {
        _loggingService = loggingService;
    }

    public void CalculateTotalAmount()
    {
        
    }
}