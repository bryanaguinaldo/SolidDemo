using SolidDemo.LoanAccounts.Enums;

namespace SolidDemo.LoanAccounts.Loans
{
    public abstract class Loan(int loanId, LoanType loanType, decimal loanAmount, double interestRate, decimal duration)
    {
        public int LoanId { get; } = loanId;
        public decimal LoanAmount { get; } = loanAmount;
        public LoanType LoanType => loanType;
        public decimal Duration => duration;
        public decimal TotalAmount() => Math.Round(LoanAmount * (decimal)Math.Pow(1 + (interestRate / 100), Convert.ToDouble(duration)), 2);
        public decimal MonthlyAmortization() => Math.Round(TotalAmount() / (duration * 12), 2);

        public virtual void OutputMessage()
        {
            Console.WriteLine();
            Console.WriteLine("Loan Details");
            Console.WriteLine($"Type: {loanType.ToString()}");
            Console.WriteLine($"Loan Amount: P{LoanAmount}");
            Console.WriteLine($"Interest Rate: {interestRate}%");
            Console.WriteLine($"Total Amount to be paid for {duration} years: P{TotalAmount()}");
            Console.WriteLine($"Monthly amortization: P{MonthlyAmortization()}");
            Console.WriteLine();
        }
    }
}