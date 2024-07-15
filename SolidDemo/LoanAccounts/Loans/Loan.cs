using SolidDemo.LoanAccounts.Interfaces;

namespace SolidDemo.LoanAccounts.Loans
{
    public abstract class Loan(int accountId, decimal loanAmount, int duration)
    {
        public int AccountId { get; } = accountId;
        public decimal LoanAmount { get; } = loanAmount;
        public int Duration { get; } = duration;
        public abstract double InterestRate { get; }
        public decimal TotalAmount() => LoanAmount * (decimal)Math.Pow(1 + InterestRate, Duration);
    }
}
