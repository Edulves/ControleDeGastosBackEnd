namespace ExpensesControl.DTOs.Responses.DataConsolidationResponses;

public class TotalExpensesResponse
{
    public decimal TotalExpense { get; set; }
    public decimal TotalExpenses { get; internal set; }
}
