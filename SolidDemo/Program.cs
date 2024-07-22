using Microsoft.Extensions.DependencyInjection;
using SolidDemo.BankAccounts.Accounts;
using SolidDemo.BankAccounts.Enums;
using SolidDemo.BankAccounts.Interfaces;
using SolidDemo.Data;
using SolidDemo.Services;

namespace SolidDemo;

internal class Program : SolidDemo
{
    static void Main(string[] args) => new SolidDemo().Start();
}
