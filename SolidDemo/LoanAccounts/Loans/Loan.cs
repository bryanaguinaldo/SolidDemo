using SolidDemo.LoanAccounts.Interfaces;

namespace SolidDemo.LoanAccounts.Loans
{
    public abstract class Loan(int loanId, decimal loanAmount, double interestRate, int duration)
    {
        public int LoanId { get; } = loanId;
        public decimal LoanAmount { get; } = loanAmount;
        public decimal TotalAmount() => LoanAmount * (decimal)Math.Pow(1 + interestRate, duration);
    }
}