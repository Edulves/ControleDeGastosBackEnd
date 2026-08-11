using ExpensesControl.Data.Context;
using ExpensesControl.Data.PaginatedResult.Extentions;
using ExpensesControl.DTOs.Requests.DailyExpensesRequests;
using ExpensesControl.Models;
using ExpensesControl.Queries;
using ExpensesControl.Repositories.RepositoryInterfaces;
using ExpensesControl.Service.ServiceInterfaces;
using Microsoft.EntityFrameworkCore;

namespace ExpensesControl.Repositories.RepositoryImplementations;

public class DailyExpensesRepository(AppDbContext context, ICurrentUserService _currentUser) : IDailyExpensesRepository
{
    public IQueryable<DailyExpense> GetDailyExpensesBase(DailyExpensesRequest request)
    {
        return context.DailyExpenses
            .FilterByUserId(_currentUser.UserId!)
            .FilterByCategory(request.Category)
            .FilterByTransactionPeriod(request.BeginningOfPeriod, request.EndOfPeriod)
            .FilterByMonthAndYear(request.Year, request.Month)
            .FilterByNote(request.Note)
            .FilterRemoveDeleted()
            .Include(x => x.TransactionCategory)
            .Include(x => x.User)
            .OrderBy(x => x.CreatedAt)
            .ThenBy(x => x.DailyExpenseId);
    }
    public async Task<decimal> GetDailyExpensesSum(DailyExpensesRequest request)
    {
        return await GetDailyExpensesBase(request).SumAsync(x => x.Amount);
    }
    public async Task<List<DailyExpense>> GetListDailyExpenses(DailyExpensesRequest request)
    {
        return await GetDailyExpensesBase(request).ToListAsync();
    }
    public async Task<(List<DailyExpense> items, int totalItems)> GetDailyExpensesPaginated(DailyExpensesRequest request)
    {
        return await GetDailyExpensesBase(request).PaginateAsync(request.Page, request.QTY);
    }
    public async Task<DailyExpense?> GetDailyExpensesById(int id)
    {
        return await context.DailyExpenses.FindAsync(id);
    }
}
