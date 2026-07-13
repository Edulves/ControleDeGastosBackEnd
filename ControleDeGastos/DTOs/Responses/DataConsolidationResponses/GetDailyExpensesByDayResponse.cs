namespace ExpensesControl.DTOs.Responses.DataConsolidationResponses;

public class GetDailyExpensesByDayResponse
{
    public DateTime InputDate {  get; set; }
    public decimal ExpenseValuePerDay { get; set; }
}
