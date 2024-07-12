using SolidDemo.Accounts;

namespace SolidDemo;

internal class Customer(int customerId, string name, IReadOnlyList<IAccount> accounts)
{
    public int CustomerId { get; } = customerId;

    public string Name { get; } = name;

    public IReadOnlyList<IAccount> Accounts { get; } = accounts;

    public IAccount GetAccount(int accountId) => Accounts.First(a => a.AccountId == accountId);
}

