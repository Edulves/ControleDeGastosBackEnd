using ExpensesControl.DTOs.Requests.DailyExpensesRequests;
using ExpensesControl.Models;

namespace ExpensesControl.Repositories.RepositoryInterfaces;

public interface IDailyExpensesRepository
{
    Task<(List<DailyExpense> items, int totalItems)> GetDailyExpensesPaginated(DailyExpensesRequest GetDailyExpenses);
    Task<List<DailyExpense>> GetListDailyExpenses(DailyExpensesRequest requisicao);
    Task<DailyExpense?> GetDailyExpensesById(int id);
    Task<decimal> GetDailyExpensesSum(DailyExpensesRequest requisicao);
}
