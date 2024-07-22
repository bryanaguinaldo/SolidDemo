using SolidDemo.Interfaces;

namespace SolidDemo.Services;

internal class LoggingService : ILoggingService
{
    public void LogMessage(string? message) => Console.WriteLine(message);
    public string? GetInput(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine();
    }
}
