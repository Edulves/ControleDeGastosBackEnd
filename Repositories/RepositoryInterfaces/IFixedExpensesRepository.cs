using ExpensesControl.DTOs.Requests.FixedExpensesRequests;
using ExpensesControl.Models;

namespace ExpensesControl.Repositories.RepositoryInterfaces;

public interface IFixedExpensesRepository
{
    Task<(List<FixedExpense> items, int totalItems)> GetFixedExpenses(GetFixedExpensesRequest requisicao);
    Task<List<FixedExpense>> GetFixedExpensesList(GetFixedExpensesRequest requisicao);
    Task<FixedExpense?> GetFixedExpensesById(int id);
    Task<decimal> GetFixedExpensesSum(GetFixedExpensesRequest requisicao);
}
