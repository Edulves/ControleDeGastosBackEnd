using ExpensesControl.Data.Contexto;
using ExpensesControl.Data.PaginatedResult.Extentions;
using ExpensesControl.DTOs.Requests.DailyExpensesRequests;
using ExpensesControl.DTOs.Requests.FixedExpensesRequests;
using ExpensesControl.Models;
using ExpensesControl.Queries;
using ExpensesControl.Repositories.InterfaceRepositories;
using Microsoft.EntityFrameworkCore;

namespace ExpensesControl.Repositories.RepositoriesImplementation
{
    public class ExpensesControlRepository(AppDbContext context) : IExpensesControlRepository
    {
        #region DailyExpenses
        public IQueryable<DailyExpense> GetDailyExpensesBase(GetDailyExpensesRequest request)
        {
            return context.DailyExpenses
            .FilterByCategory(request.Category)
            .FilterByTransactionPeriod(request.BeginningOfPeriod, request.EndOfPeriod)
            .FilterByMonthAndYear(request.Year, request.Month)
            .FilterByNote(request.Note)
            .FilterRemoveDeleted()
            .Include(x => x.TransactionCategory)
            .OrderBy(x => x.InputDate)
            .ThenBy(x => x.DailyExpensesId);
        }
        public async Task<decimal> GetDailyExpensesSum(GetDailyExpensesRequest request)
        {
            return await GetDailyExpensesBase(request).SumAsync(x => x.ExpenseValue);
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
        #endregion

        #region TransactionCategory
        public IQueryable<TransactionCategory> GetTransactionCategoriesBase()
        {
            return context.TransactionCategories.FilterRemoveDeleted();
        }
        public async Task<List<TransactionCategory>> GetTransactionCategories()
        {
            return await GetTransactionCategoriesBase().OrderBy(x => x.CategoryName).ToListAsync();
        }
        public async Task<TransactionCategory?> GetTransactionCategoryById(int id)
        {
            return await context.TransactionCategories.FindAsync(id);
        }
        #endregion

        #region FixedExpenses
        public IQueryable<FixedExpense> GetFixedExpensesBase(GetFixedExpensesRequest request)
        {
            return context.FixedExpenses
            .FilterRemoveDeleteds()
            .FilterByDescription(request.ExpenseDescription)
            .FilterByMonthAndYear(request.Year, request.Month)
            .FilterByPeriod(request.BeginningOfPeriod, request.EndOfPeriod)
            .OrderBy(x => x.InputDate)
            .ThenBy(x => x.FixedExpenseId);
        }
        public async Task<List<FixedExpense>> GetFixedExpensesList(GetFixedExpensesRequest request)
        {
            return await GetFixedExpensesBase(request).ToListAsync();
        }
        public async Task<decimal> GetFixedExpensesSum(GetFixedExpensesRequest request)
        {
            return await  GetFixedExpensesBase(request).SumAsync(x => x.FixedExpenseValue);
        }
        public async Task<(List<FixedExpense> items, int totalItems)> GetFixedExpenses(GetFixedExpensesRequest request)
        {
            return await GetFixedExpensesBase(request).PaginateAsync(request.Page, request.QTY);
        }
        public async Task<FixedExpense?> GetFixedExpensesById(int id)
        {
            return await context.FixedExpenses.FindAsync(id);
        }
        #endregion
    }
}
