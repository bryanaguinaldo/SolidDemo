using SolidDemo.Interfaces;
using SolidDemo.LoanAccounts.Enums;
using SolidDemo.LoanAccounts.Interfaces;

namespace SolidDemo.LoanAccounts.Loans
{
    public abstract class Loan(int loanId, LoanType loanType, decimal loanAmount, double interestRate, int duration)
    {
        public int LoanId { get; } = loanId;
        public decimal LoanAmount { get; } = loanAmount;
        public LoanType LoanType => loanType;
        public int Duration => duration;
        public decimal TotalAmount() => Math.Round(LoanAmount * (decimal)Math.Pow(1 + interestRate, duration), 2);
        public decimal MonthlyAmortization() => Math.Round(TotalAmount() / duration, 2);

        public void OutputMessage()
        {
            Console.WriteLine();
            Console.WriteLine("Loan Details");
            Console.WriteLine($"Type: {loanType.ToString()}");
            Console.WriteLine($"Loan Amount: P{LoanAmount}");
            Console.WriteLine($"Interest Rate: {interestRate * 100}%");
            Console.WriteLine($"Total Amount to be paid for {duration} months: P{TotalAmount()}");
            Console.WriteLine($"Monthly amortization: P{MonthlyAmortization()}");
            Console.WriteLine();
        }
    }
}