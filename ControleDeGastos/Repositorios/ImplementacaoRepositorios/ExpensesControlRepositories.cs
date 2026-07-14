using ExpensesControl.Data.Contexto;
using ExpensesControl.Data.PaginatedResult.Extentions;
using ExpensesControl.DTOs.Requests.DailyExpensesRequests;
using ExpensesControl.DTOs.Requests.FixedExpensesRequests;
using ExpensesControl.Models;
using ExpensesControl.Queries;
using ExpensesControl.Repositories.RepositoriesInterface;
using Microsoft.EntityFrameworkCore;

namespace ExpensesControl.Repositories.RepositoriesImplementation
{
    public class ExpensesControlRepositories(AppDbContext context) : IExpensesControlRepositories
    {
        #region GastosDiarios
        public IQueryable<DailyExpenses> ObterGastosDiariosBase(GetDailyExpensesRequest requisicao)
        {
            return context.gastos_diarios
            .FilterByCategory(requisicao.Category)
            .FilterByTransactionPeriod(requisicao.BeginningOfPeriod, requisicao.EndOfPeriod)
            .FilterByMonthAndYear(requisicao.Year, requisicao.Month)
            .FilterByNote(requisicao.Note)
            .FilterRemoveDeleted()
            .Include(x => x.Category)
            .OrderBy(x => x.InputDate)
            .ThenBy(x => x.DailyExpensesId);
        }
        public async Task<decimal> ObterSomaGastosDiarios(GetDailyExpensesRequest requisicao)
        {
            return await ObterGastosDiariosBase(requisicao).SumAsync(x => x.ExpenseValue);
        }
        public async Task<List<DailyExpenses>> ObterGastosDiariosLista(GetDailyExpensesRequest requisicao)
        {
            return await ObterGastosDiariosBase(requisicao).ToListAsync();
        }
        public async Task<(List<DailyExpenses> itens, int totalItens)> ObterGastosDiariosPaginado(GetDailyExpensesRequest requisicao)
        {
            return await ObterGastosDiariosBase(requisicao).PaginateAsync(requisicao.Page, requisicao.QTY);
        }
        public async Task<DailyExpenses?> ObterGastoDiarioPorId(int id)
        {
            return await context.gastos_diarios.FindAsync(id);
        }
        #endregion

        #region CategoriasDeGastos
        public IQueryable<TransactionCategories> ObterCategoriasDeLancamentosBase()
        {
            return context.categorias_de_lancamentos.FilterRemoveDeleted();
        }
        public async Task<List<TransactionCategories>> ObterCategoriasDeLancamentos()
        {
            return await ObterCategoriasDeLancamentosBase().OrderBy(x => x.CategoryName).ToListAsync();
        }
        public async Task<TransactionCategories?> ObterCategoriasDeLancamentosPorId(int id)
        {
            return await context.categorias_de_lancamentos.FindAsync(id);
        }
        #endregion

        #region GastosFixos
        public IQueryable<FixedExpenseResult> ObterGastosFixosBase(GetFixedExpensesRequest requisicao)
        {
            return context.gastos_fixos
            .FilterRemoveDeleteds()
            .FilterByDescription(requisicao.ExpenseDescription)
            .FilterByMonthAndYear(requisicao.Year, requisicao.Month)
            .FilterByPeriod(requisicao.BeginningOfPeriod, requisicao.EndOfPeriod)
            .OrderBy(x => x.InputDate)
            .ThenBy(x => x.FixedExpenseId);
        }
        public async Task<List<FixedExpenseResult>> ObterGastosFixosLista(GetFixedExpensesRequest requisicao)
        {
            return await ObterGastosFixosBase(requisicao).ToListAsync();
        }
        public async Task<decimal> ObterSomaGastosFixos(GetFixedExpensesRequest requisicao)
        {
            return await  ObterGastosFixosBase(requisicao).SumAsync(x => x.FixedExpenseValue);
        }
        public async Task<(List<FixedExpenseResult> itens, int totalItens)> ObterGastosFixos(GetFixedExpensesRequest requisicao)
        {
            return await ObterGastosFixosBase(requisicao).PaginarAsync(requisicao.Page, requisicao.QTY);
        }
        public async Task<FixedExpenseResult?> ObterGastosFixosPorId(int id)
        {
            return await context.gastos_fixos.FindAsync(id);
        }
        #endregion
    }
}
