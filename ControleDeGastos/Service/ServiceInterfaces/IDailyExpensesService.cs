using ExpensesControl.Data.PaginatedResult;
using ExpensesControl.Data.ResultPattern.Base;
using ExpensesControl.DTOs.Requests.DailyExpensesRequests;
using ExpensesControl.DTOs.Responses.DailyExpensesReponses;

namespace ExpensesControl.Service.ServiceInterfaces;

public interface IDailyExpensesService
{
    Task<ResultPattern<string>> CreateDailyExpensesEntriesAsync(List<DailyExpenseEntryRequest> requisicao);
    Task<ResultPattern<PagedResult<DailyExpenseResponse>>> GetDailyExpensesAsync(GetDailyExpensesRequest requisicao);
    Task<ResultPattern<string>> UpdateDailyExpensesEntriesAsync(List<PutDailyExpensesRequest> requisicao);
    Task<ResultPattern<string>> DeleteDailyExpenseEntryByIdAsync(int id);
}
