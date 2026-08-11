using ExpensesControl.Data.Context;
using ExpensesControl.Data.PaginatedResult.Extentions;
using ExpensesControl.DTOs.Requests.FixedExpensesRequests;
using ExpensesControl.Models;
using ExpensesControl.Queries;
using ExpensesControl.Repositories.RepositoryInterfaces;
using ExpensesControl.Service.ServiceInterfaces;
using Microsoft.EntityFrameworkCore;

namespace ExpensesControl.Repositories.RepositoryImplementations;

public class FixedExpensesRepository(AppDbContext context, ICurrentUserService _currentUser) : IFixedExpensesRepository
{
    public IQueryable<FixedExpense> GetFixedExpensesBase(GetFixedExpensesRequest request)
    {
        return context.FixedExpenses
        .FilterByUserId(_currentUser.UserId!)
        .FilterRemoveDeleteds()
        .FilterByDescription(request.ExpenseDescription)
        .FilterByMonthAndYear(request.Year, request.Month)
        .FilterByPeriod(request.BeginningOfPeriod, request.EndOfPeriod)
        .Include(x => x.User)
        .OrderBy(x => x.CreatedAt)
        .ThenBy(x => x.FixedExpenseId);
    }
    public async Task<List<FixedExpense>> GetFixedExpensesList(GetFixedExpensesRequest request)
    {
        return await GetFixedExpensesBase(request).ToListAsync();
    }
    public async Task<decimal> GetFixedExpensesSum(GetFixedExpensesRequest request)
    {
        return await GetFixedExpensesBase(request).SumAsync(x => x.Amount);
    }
    public async Task<(List<FixedExpense> items, int totalItems)> GetFixedExpenses(GetFixedExpensesRequest request)
    {
        return await GetFixedExpensesBase(request).PaginateAsync(request.Page, request.QTY);
    }
    public async Task<FixedExpense?> GetFixedExpensesById(int id)
    {
        return await context.FixedExpenses.FindAsync(id);
    }
}
