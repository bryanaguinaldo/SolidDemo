using Microsoft.Extensions.DependencyInjection;

namespace SolidDemo;

public class BaseDemo
{
    public BaseDemo()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.RegisterBankAccountServices();

        ServiceProvider = serviceCollection.BuildServiceProvider();
    }

    public ServiceProvider ServiceProvider { get; }
}