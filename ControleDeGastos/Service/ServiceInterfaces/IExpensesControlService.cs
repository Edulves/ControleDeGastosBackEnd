using ExpensesControl.Data.PaginatedResult;
using ExpensesControl.Data.ResultPattern.Base;
using ExpensesControl.DTOs.Requests.CategoriesRequests;
using ExpensesControl.DTOs.Requests.DailyExpensesRequests;
using ExpensesControl.DTOs.Requests.DataConsolidationRequests;
using ExpensesControl.DTOs.Requests.FixedExpensesRequests;
using ExpensesControl.DTOs.Responses.DailyExpensesReponses;
using ExpensesControl.DTOs.Responses.DataConsolidationResponses;
using ExpensesControl.Models;

namespace ExpensesControl.Service.ServiceInterfaces
{
    public interface IExpensesControlService
    {
        #region GastosDiarios
        Task<ResultPattern<string>> CreateDailyExpensesEntriesAsync(List<DailyExpenseEntryRequest> requisicao);
        Task<ResultPattern<PagedResult<DailyExpenseResponse>>> GetDailyExpensesAsync(GetDailyExpensesRequest requisicao);
        Task<ResultPattern<string>> UpdateDailyExpensesEntriesAsync(List<PutDailyExpensesRequest> requisicao);
        Task<ResultPattern<string>> DeleteDailyExpenseEntryByIdAsync(int id);
        #endregion

        #region CategoriasDeGastos
        Task<ResultPattern<List<TransactionCategory>>> GetEntryCategoriesAsync();
        Task<ResultPattern<string>> CreateCategoriesAsync(List<CreateCategoryRequest> requisicao);
        Task<ResultPattern<string>> PutCategoriesAsync(List<TransactionCategory> requisicao);
        Task<ResultPattern<string>> DeleteCategoryByIdAsync(int id);
        #endregion

        #region GastosFixos
        Task<ResultPattern<PagedResult<FixedExpense>>> GetFixedExpensesAsync(GetFixedExpensesRequest requisicao);
        Task<ResultPattern<string>> PostFixedExpenseAsync(List<PostFixedExpensesDto> requisicao);
        Task<ResultPattern<string>> PutFixedExpensesAsync(List<PutFixedExpensesRequest> requisicao);
        Task<ResultPattern<string>> DeleteFixedExpensesAsync(int id);
        #endregion

        #region Consolidado
        Task<ResultPattern<DailyExpensesPerCategoryResult>> GetExpensesSumPerCategoryAsync(GetByFullDateOrMothAndYearRequest requisicao);
        Task<ResultPattern<DailyExpensesConsolidationResult>> GetExpensesSumPerDayAsync(ExpensesByMothAndYearRequest requisicao);
        Task<ResultPattern<TotalFixedExpensesComparasionResponse>> GetTotalFixedExpensesComparasionAsync(ExpensesByMothAndYearRequest requisicao);
        Task<ResultPattern<TotalExpensesResponse>> GetTotalDailyExpensesAsync(ExpensesByMothAndYearRequest requisicao);
        #endregion
    }
}
