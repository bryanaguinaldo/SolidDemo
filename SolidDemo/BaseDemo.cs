using Microsoft.Extensions.DependencyInjection;

namespace SolidDemo;

public abstract class BaseDemo
{
    public BaseDemo()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.RegisterBankAccountServices();

        ServiceProvider = serviceCollection.BuildServiceProvider();
    }

    public ServiceProvider ServiceProvider { get; }
}