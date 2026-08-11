using Microsoft.AspNetCore.Identity;

namespace ExpensesControl.DTOs.Responses.DailyExpensesReponses;

public class DailyExpenseResponse
{
    public int DailyExpenseId { get; set; }
    public DateOnly ExpenseDate { get; set; }
    public decimal ExpenseValue { get; set; }
    public string Note { get; set; } = string.Empty;
    public string CategoryName { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public IdentityUser User { get; set; } = new IdentityUser();
}
