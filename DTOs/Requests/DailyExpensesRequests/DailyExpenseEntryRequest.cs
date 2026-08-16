using System.ComponentModel.DataAnnotations;

namespace ExpensesControl.DTOs.Requests.DailyExpensesRequests;

public class DailyExpenseEntryRequest
{
    [Required]
    public DateOnly ExpenseDate { get; set; }
    [Required]
    public decimal Amount { get; set; }
    public string? Note { get; set; }
    [Required]
    public int CategoryId { get; set; }
}
