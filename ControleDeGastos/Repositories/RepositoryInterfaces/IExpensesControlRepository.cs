using ExpensesControl.DTOs.Requests.DailyExpensesRequests;
using ExpensesControl.DTOs.Requests.FixedExpensesRequests;
using ExpensesControl.Models;

namespace ExpensesControl.Repositories.InterfaceRepositories;

public interface IExpensesControlRepository
{
    #region DailyExpenses
    Task<(List<DailyExpense> items, int totalItems)> GetDailyExpensesPaginated(GetDailyExpensesRequest GetDailyExpenses);
    Task<List<DailyExpense>> GetListDailyExpenses(GetDailyExpensesRequest requisicao);
    Task<DailyExpense?> GetDailyExpensesById(int id);
    Task<decimal> GetDailyExpensesSum(GetDailyExpensesRequest requisicao);
    #endregion

    #region CategoriasDeGastos
    Task<List<TransactionCategory>> GetTransactionCategories();
    Task<TransactionCategory?> GetTransactionCategoryById(int id);
    #endregion

    #region GastosFixos
    Task<(List<FixedExpense> items, int totalItems)> GetFixedExpenses(GetFixedExpensesRequest requisicao);
    Task<List<FixedExpense>> GetFixedExpensesList(GetFixedExpensesRequest requisicao);
    Task<FixedExpense?> GetFixedExpensesById(int id);
    Task<decimal> GetFixedExpensesSum(GetFixedExpensesRequest requisicao);
    #endregion
}
