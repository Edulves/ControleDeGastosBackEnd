using System.ComponentModel.DataAnnotations;

namespace ExpensesControl.DTOs.Requests.FixedExpensesRequests;

public class PutFixedExpensesRequest
{
    [Required]
    public int FixedExpensesId { get; set; }
    public string FixedExpenseDescription { get; set; } = string.Empty;
    public decimal FixedExpenseValue { get; set; } = 0;
    public bool Paid { get; set; } = false;
    public DateTime InputDate { get; set; } = DateTime.MinValue;
}
