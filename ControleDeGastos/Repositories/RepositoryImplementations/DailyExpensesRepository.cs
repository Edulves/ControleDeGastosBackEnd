using ExpensesControl.Data.Context;
using ExpensesControl.Data.PaginatedResult.Extentions;
using ExpensesControl.DTOs.Requests.DailyExpensesRequests;
using ExpensesControl.Models;
using ExpensesControl.Queries;
using ExpensesControl.Repositories.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace ExpensesControl.Repositories.RepositoryImplementations;

public class DailyExpensesRepository(AppDbContext context) : IDailyExpensesRepository
{
    public IQueryable<DailyExpense> GetDailyExpensesBase(GetDailyExpensesRequest request)
    {
        return context.DailyExpenses
            .FilterByCategory(request.Category)
            .FilterByTransactionPeriod(request.BeginningOfPeriod, request.EndOfPeriod)
            .FilterByMonthAndYear(request.Year, request.Month)
            .FilterByNote(request.Note)
            .FilterRemoveDeleted()
            .Include(x => x.TransactionCategory)
            .OrderBy(x => x.CreatedAt)
            .ThenBy(x => x.DailyExpenseId);
    }
    public async Task<decimal> GetDailyExpensesSum(GetDailyExpensesRequest request)
    {
        return await GetDailyExpensesBase(request).SumAsync(x => x.Amount);
    }
    public async Task<List<DailyExpense>> GetListDailyExpenses(GetDailyExpensesRequest request)
    {
        return await GetDailyExpensesBase(request).ToListAsync();
    }
    public async Task<(List<DailyExpense> items, int totalItems)> GetDailyExpensesPaginated(GetDailyExpensesRequest request)
    {
        return await GetDailyExpensesBase(request).PaginateAsync(request.Page, request.QTY);
    }
    public async Task<DailyExpense?> GetDailyExpensesById(int id)
    {
        return await context.DailyExpenses.FindAsync(id);
    }
}
