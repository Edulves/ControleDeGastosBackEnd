namespace ExpensesControl.DTOs.Responses.DataConsolidationResponses;

public class DailyExpensesPerCategoryResult
{
    public List<GetDailyExpensesByCategoryReponse> DailyExpensesByCategoryList { get; set; } = [];
    public decimal Total { get; set; }
}
