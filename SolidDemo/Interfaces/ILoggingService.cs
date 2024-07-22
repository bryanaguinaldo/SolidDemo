namespace SolidDemo.Interfaces;

public interface ILoggingService
{
    void LogMessage(string? message = null);
    string GetInput(string prompt);
}