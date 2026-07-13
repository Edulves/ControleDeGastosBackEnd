using System.ComponentModel.DataAnnotations;

namespace ExpensesControl.DTOs.Requests.DailyExpensesRequests;

public class DailyExpenseEntryRequest
{
    [Required]
    public DateTime InputDate { get; set; }
    [Required]
    public decimal ExpenseValue { get; set; }
    public string? Note { get; set; }
    [Required]
    public int CategoryId { get; set; }
}
