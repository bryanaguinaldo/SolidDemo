using Microsoft.Extensions.DependencyInjection;
using SolidDemo.BankAccounts.Interfaces;
using SolidDemo.BankAccounts.Validations;
using SolidDemo.Interfaces;
using SolidDemo.Services;

namespace SolidDemo
{
    public static class DependencyRegistration
    {
        public static IServiceCollection RegisterLoginServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<ILoginService, LoginService>();

            return serviceCollection;
        }

        public static IServiceCollection RegisterLoggingServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<ILoggingService, LoggingService>();

            return serviceCollection;
        }

        public static IServiceCollection RegisterBankAccountServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IBankService, BankService>();
            serviceCollection.AddScoped<IAccountValidation, SavingsAccountValidation>();
            serviceCollection.AddScoped<IAccountValidation, CurrentAccountValidation>();
            serviceCollection.AddScoped<IAccountValidation, TimeDepositValidation>();
            serviceCollection.AddScoped<IAccountValidation, DollarAccountValidation>();

            return serviceCollection;
        }

        public static IServiceCollection RegisterLoanAccountServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<ILoanService, LoanService>();

            return serviceCollection;
        }
    }
}