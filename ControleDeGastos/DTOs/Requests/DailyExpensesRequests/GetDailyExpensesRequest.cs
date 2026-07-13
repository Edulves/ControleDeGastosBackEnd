using ExpensesControl.Data.PaginatedResult.PaginatedRequestDTO;

namespace ExpensesControl.DTOs.Requests.DailyExpensesRequests
{
    public class GetDailyExpensesRequest : PaginatedRequest
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public DateTime BeginningOfPeriod { get; set; } = DateTime.MinValue;
        public DateTime EndOfPeriod { get; set; } = DateTime.MinValue;
        public string Category { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;
    }
}
