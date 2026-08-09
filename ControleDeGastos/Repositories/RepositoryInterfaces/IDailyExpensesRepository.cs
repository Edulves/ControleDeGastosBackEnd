using ExpensesControl.DTOs.Requests.DailyExpensesRequests;
using ExpensesControl.Models;

namespace ExpensesControl.Repositories.RepositoryInterfaces;

public interface IDailyExpensesRepository
{
    Task<(List<DailyExpense> items, int totalItems)> GetDailyExpensesPaginated(GetDailyExpensesRequest GetDailyExpenses);
    Task<List<DailyExpense>> GetListDailyExpenses(GetDailyExpensesRequest requisicao);
    Task<DailyExpense?> GetDailyExpensesById(int id);
    Task<decimal> GetDailyExpensesSum(GetDailyExpensesRequest requisicao);
}
