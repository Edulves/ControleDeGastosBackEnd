using ExpensesControl.Data.PaginatedResult;
using ExpensesControl.Data.ResultPattern.Base;
using ExpensesControl.DTOs.Requests.FixedExpensesRequests;
using ExpensesControl.Models;

namespace ExpensesControl.Service.ServiceInterfaces;

public interface IFixedExpensesService
{
    Task<ResultPattern<PagedResult<FixedExpense>>> GetFixedExpensesAsync(GetFixedExpensesRequest requisicao);
    Task<ResultPattern<string>> PostFixedExpenseAsync(List<PostFixedExpensesDto> requisicao);
    Task<ResultPattern<string>> PutFixedExpensesAsync(List<PutFixedExpensesRequest> requisicao);
    Task<ResultPattern<string>> DeleteFixedExpensesAsync(int id);
}
