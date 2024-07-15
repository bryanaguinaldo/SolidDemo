using SolidDemo.BankAccounts.Interfaces;

namespace SolidDemo.Services;

internal class LoggingService : ILoggingService
{
    public void LogMessage(string message) => Console.WriteLine(message);
}
