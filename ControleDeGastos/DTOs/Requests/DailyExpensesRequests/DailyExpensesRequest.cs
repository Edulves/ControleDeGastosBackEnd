using ExpensesControl.Data.PaginatedResult.PaginatedRequestDTO;
using System.ComponentModel.DataAnnotations;

namespace ExpensesControl.DTOs.Requests.DailyExpensesRequests
{
    public class DailyExpensesRequest : PaginatedRequest
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public DateOnly BeginningOfPeriod { get; set; } = DateOnly.MinValue;
        public DateOnly EndOfPeriod { get; set; } = DateOnly.MinValue;
        public string Category { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;
    }
}
