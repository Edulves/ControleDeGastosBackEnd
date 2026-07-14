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
        public IQueryable<DailyExpenses> GetDailyExpensesBase(GetDailyExpensesRequest request)
        {
            return context.gastos_diarios
            .FilterByCategory(request.Category)
            .FilterByTransactionPeriod(request.BeginningOfPeriod, request.EndOfPeriod)
            .FilterByMonthAndYear(request.Year, request.Month)
            .FilterByNote(request.Note)
            .FilterRemoveDeleted()
            .Include(x => x.Category)
            .OrderBy(x => x.InputDate)
            .ThenBy(x => x.DailyExpensesId);
        }
        public async Task<decimal> GetDailyExpensesSum(GetDailyExpensesRequest request)
        {
            return await GetDailyExpensesBase(request).SumAsync(x => x.ExpenseValue);
        }
        public async Task<List<DailyExpenses>> GetListDailyExpenses(GetDailyExpensesRequest request)
        {
            return await GetDailyExpensesBase(request).ToListAsync();
        }
        public async Task<(List<DailyExpenses> items, int totalItems)> GetDailyExpensesPaginated(GetDailyExpensesRequest request)
        {
            return await GetDailyExpensesBase(request).PaginateAsync(request.Page, request.QTY);
        }
        public async Task<DailyExpenses?> GetDailyExpensesById(int id)
        {
            return await context.gastos_diarios.FindAsync(id);
        }
        #endregion

        #region TransactionCategory
        public IQueryable<TransactionCategories> GetTransactionCategoriesBase()
        {
            return context.categorias_de_lancamentos.FilterRemoveDeleted();
        }
        public async Task<List<TransactionCategories>> GetTransactionCategories()
        {
            return await GetTransactionCategoriesBase().OrderBy(x => x.CategoryName).ToListAsync();
        }
        public async Task<TransactionCategories?> GetTransactionCategoryById(int id)
        {
            return await context.categorias_de_lancamentos.FindAsync(id);
        }
        #endregion

        #region FixedExpenses
        public IQueryable<FixedExpenseResult> GetFixedExpensesBase(GetFixedExpensesRequest request)
        {
            return context.gastos_fixos
            .FilterRemoveDeleteds()
            .FilterByDescription(request.ExpenseDescription)
            .FilterByMonthAndYear(request.Year, request.Month)
            .FilterByPeriod(request.BeginningOfPeriod, request.EndOfPeriod)
            .OrderBy(x => x.InputDate)
            .ThenBy(x => x.FixedExpenseId);
        }
        public async Task<List<FixedExpenseResult>> GetFixedExpensesList(GetFixedExpensesRequest request)
        {
            return await GetFixedExpensesBase(request).ToListAsync();
        }
        public async Task<decimal> GetFixedExpensesSum(GetFixedExpensesRequest request)
        {
            return await  GetFixedExpensesBase(request).SumAsync(x => x.FixedExpenseValue);
        }
        public async Task<(List<FixedExpenseResult> items, int totalItems)> GetFixedExpenses(GetFixedExpensesRequest request)
        {
            return await GetFixedExpensesBase(request).PaginateAsync(request.Page, request.QTY);
        }
        public async Task<FixedExpenseResult?> GetFixedExpensesById(int id)
        {
            return await context.gastos_fixos.FindAsync(id);
        }
        #endregion
    }
}
