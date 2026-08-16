using Microsoft.AspNetCore.Identity;

namespace ExpensesControl.Models;

public class DailyExpense : Entity
{
    public int DailyExpenseId { get; set; }
    public DateOnly ExpenseDate { get; set; }
    public decimal Amount { get; set; }
    public string? Note { get; set; }
    public int? TransactionCategoryId { get; set; }
    public string UserId { get; set; } = string.Empty;
    public TransactionCategory? TransactionCategory { get; set; }
    public IdentityUser? User { get; set; } = null!;
}
