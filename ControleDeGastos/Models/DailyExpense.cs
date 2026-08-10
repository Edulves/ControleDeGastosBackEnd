namespace ExpensesControl.Models;

public class DailyExpense : Entity
{
    public int DailyExpenseId { get; set; }
    public DateOnly ExpenseDate { get; set; }
    public decimal Amount { get; set; }
    public string? Note { get; set; }
    public int? TransactionCategoryId { get; set; }
    public TransactionCategory? TransactionCategory { get; set; }
}
