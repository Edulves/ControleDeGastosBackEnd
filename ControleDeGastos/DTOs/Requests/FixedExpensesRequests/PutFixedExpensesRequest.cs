using System.ComponentModel.DataAnnotations;

namespace ExpensesControl.DTOs.Requests.FixedExpensesRequests;

public class PutFixedExpensesRequest
{
    [Required]
    public int FixedExpensesId { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Amount { get; set; } = 0;
    public bool IsPaid { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.MinValue;
}
