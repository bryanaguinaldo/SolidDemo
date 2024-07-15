using SolidDemo.LoanAccounts.Enums;

namespace SolidDemo.LoanAccounts.Interfaces
{
    internal interface ILoan
    {
        public decimal TotalAmount();
        LoanType LoanType { get; }
    }
}
