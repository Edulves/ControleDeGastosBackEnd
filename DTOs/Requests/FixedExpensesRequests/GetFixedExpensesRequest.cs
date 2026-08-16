using ExpensesControl.Data.PaginatedResult.PaginatedRequestDTO;

namespace ExpensesControl.DTOs.Requests.FixedExpensesRequests;

public class GetFixedExpensesRequest : PaginatedRequest
{
    public DateOnly BeginningOfPeriod { get; set; }
    public DateOnly EndOfPeriod { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }
    public string ExpenseDescription { get; set; } = string.Empty;
}
