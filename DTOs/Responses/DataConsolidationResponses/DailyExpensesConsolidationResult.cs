namespace ExpensesControl.DTOs.Responses.DataConsolidationResponses;

public class DailyExpensesConsolidationResult
{
    public List<GetDailyExpensesByDayResponse> DailyExpensesList { get; set; } = [];
    public decimal Total { get; set; }
}
