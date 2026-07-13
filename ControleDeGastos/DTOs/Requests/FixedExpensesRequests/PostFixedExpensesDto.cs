using System.ComponentModel.DataAnnotations;

namespace ExpensesControl.DTOs.Requests.FixedExpensesRequests
{
    public class PostFixedExpensesDto
    {
        [Required]
        [Length(3, 100, ErrorMessage = "The fixed expense description must be between 3 and 100 characters long")]
        public string FixedExpenseDescription { get; set; } = string.Empty;
        [Required]
        public decimal FixedExpenseValue { get; set; }
        [Required]
        public DateTime InputDate { get; set; }
    }
}
