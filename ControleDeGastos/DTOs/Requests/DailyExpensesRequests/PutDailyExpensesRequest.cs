using System.ComponentModel.DataAnnotations;

namespace ExpensesControl.DTOs.Requests.DailyExpensesRequests;

public class PutDailyExpensesRequest
{   
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "DailyExpenseId must be greater than 0")]
    public int DailyExpenseId { get; set; }
    public DateOnly ExpenseDate  { get; set; } = DateOnly.MinValue;
    public decimal ExpenseValue { get; set; } = decimal.Zero;
    public string Note { get; set; } = string.Empty;
    public int CategoryId { get; set; } = int.MinValue;
}
