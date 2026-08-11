namespace ExpensesControl.DTOs.Requests.DailyExpensesRequests;

public class DeleteDailyExpenseRequest
{
    public int EntryId { get; set; }
    public string UserId { get; set; } = string.Empty;
}
