namespace ExpensesControl.DTOs.Responses.DataConsolidationResponses;

public class GetDailyExpensesByCategoryReponse
{
    public string CategoryName { get; set; } = string.Empty;
    public decimal ExpenseValue { get; set; }
}
