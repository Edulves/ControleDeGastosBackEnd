using ExpensesControl.DTOs.Requests.DailyExpensesRequests;
using ExpensesControl.DTOs.Requests.FixedExpensesRequests;
using ExpensesControl.Models;

namespace ExpensesControl.Repositories.InterfaceRepositories;

public interface IExpensesControlRepository
{
    #region DailyExpenses
    Task<(List<DailyExpenses> items, int totalItems)> GetDailyExpensesPaginated(GetDailyExpensesRequest GetDailyExpenses);
    Task<List<DailyExpenses>> GetListDailyExpenses(GetDailyExpensesRequest requisicao);
    Task<DailyExpenses?> GetDailyExpensesById(int id);
    Task<decimal> GetDailyExpensesSum(GetDailyExpensesRequest requisicao);
    #endregion

    #region CategoriasDeGastos
    Task<List<TransactionCategories>> GetTransactionCategories();
    Task<TransactionCategories?> GetTransactionCategoryById(int id);
    #endregion

    #region GastosFixos
    Task<(List<FixedExpenseResult> items, int totalItems)> GetFixedExpenses(GetFixedExpensesRequest requisicao);
    Task<List<FixedExpenseResult>> GetFixedExpensesList(GetFixedExpensesRequest requisicao);
    Task<FixedExpenseResult?> GetFixedExpensesById(int id);
    Task<decimal> GetFixedExpensesSum(GetFixedExpensesRequest requisicao);
    #endregion
}
