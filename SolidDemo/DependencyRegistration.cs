using Microsoft.Extensions.DependencyInjection;
using SolidDemo.BankAccounts.Interfaces;
using SolidDemo.BankAccounts.Validations;
using SolidDemo.Interfaces;
using SolidDemo.Services;

namespace SolidDemo
{
    public static class DependencyRegistration
    {
        public static IServiceCollection RegisterBankAccountServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<ILoggingService, LoggingService>();
            serviceCollection.AddScoped<IBankService, BankService>();
            serviceCollection.AddScoped<ILoanService, LoanService>();
            serviceCollection.AddScoped<IAccountValidation, SavingsAccountValidation>();
            serviceCollection.AddScoped<IAccountValidation, CurrentAccountValidation>();
            serviceCollection.AddScoped<IAccountValidation, TimeDepositValidation>();
            serviceCollection.AddScoped<IAccountValidation, DollarAccountValidation>();

            return serviceCollection;
        }

        public static IServiceCollection RegisterLoanAccountServices(this IServiceCollection serviceCollection)
        {
            return serviceCollection.AddScoped<ILoanService, LoanService>();
        }
    }
}