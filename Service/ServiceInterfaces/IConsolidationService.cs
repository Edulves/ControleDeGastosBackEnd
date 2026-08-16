using ExpensesControl.Data.ResultPattern.Base;
using ExpensesControl.DTOs.Requests.DailyExpensesRequests;
using ExpensesControl.DTOs.Requests.DataConsolidationRequests;
using ExpensesControl.DTOs.Responses.DataConsolidationResponses;
using ExpensesControl.Models;

namespace ExpensesControl.Service.ServiceInterfaces;

public interface IConsolidationService
{
    Task<ResultPattern<DailyExpensesPerCategoryResult>> GetExpensesSumPerCategoryAsync(GetByFullDateOrMothAndYearRequest requisicao);
    Task<ResultPattern<DailyExpensesConsolidationResult>> GetExpensesSumPerDayAsync(ExpensesByMothAndYearRequest requisicao);
    Task<ResultPattern<TotalFixedExpensesComparasionResponse>> GetTotalFixedExpensesComparasionAsync(ExpensesByMothAndYearRequest requisicao);
    Task<ResultPattern<TotalExpensesResponse>> GetTotalDailyExpensesAsync(ExpensesByMothAndYearRequest requisicao);
}
