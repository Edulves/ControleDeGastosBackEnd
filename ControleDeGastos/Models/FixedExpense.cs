namespace ExpensesControl.Models;

public class FixedExpense : Entity
{
    public int FixedExpenseId { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public bool IsPaid { get; set; } = false;
    public DateOnly FixedExpenseDate { get; set; } 
}