using ExpensesControl.Data.Contexto;
using ExpensesControl.Data.PaginatedResult.Extentions;
using ExpensesControl.DTOs.Requests.DailyExpensesRequests;
using ExpensesControl.DTOs.Requests.FixedExpensesRequests;
using ExpensesControl.Modelos;
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
            .FiltrarPorCategorias(requisicao.Category)
            .FiltrarPorPeriodoDeLancamento(requisicao.BeginningOfPeriod, requisicao.EndOfPeriod)
            .FiltrarPorMeseAno(requisicao.Year, requisicao.Month)
            .FiltrarPorObservacao(requisicao.Note)
            .FiltrarRemoverDeletados()
            .Include(x => x.categoria)
            .OrderBy(x => x.DataDoLancamento)
            .ThenBy(x => x.IdGastosDiarios);
        }
        public async Task<decimal> ObterSomaGastosDiarios(GetDailyExpensesRequest requisicao)
        {
            return await ObterGastosDiariosBase(requisicao).SumAsync(x => x.Valorgasto);
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
        public IQueryable<EntryCategories> ObterCategoriasDeLancamentosBase()
        {
            return context.categorias_de_lancamentos.FiltrarRemoverDeletados();
        }
        public async Task<List<EntryCategories>> ObterCategoriasDeLancamentos()
        {
            return await ObterCategoriasDeLancamentosBase().OrderBy(x => x.NomeDaCategoria).ToListAsync();
        }
        public async Task<EntryCategories?> ObterCategoriasDeLancamentosPorId(int id)
        {
            return await context.categorias_de_lancamentos.FindAsync(id);
        }
        #endregion

        #region GastosFixos
        public IQueryable<FixedExpenseResult> ObterGastosFixosBase(GetFixedExpensesRequest requisicao)
        {
            return context.gastos_fixos
            .FiltrarRemoverDeletados()
            .FiltrarPorDescricao(requisicao.ExpenseDescription)
            .FiltrarPorMeseAno(requisicao.Year, requisicao.Month)
            .FiltrarPorPeriodo(requisicao.BeginningOfPeriod, requisicao.EndOfPeriod)
            .OrderBy(x => x.DataDoLancamento)
            .ThenBy(x => x.IdGastosFixos);
        }
        public async Task<List<FixedExpenseResult>> ObterGastosFixosLista(GetFixedExpensesRequest requisicao)
        {
            return await ObterGastosFixosBase(requisicao).ToListAsync();
        }
        public async Task<decimal> ObterSomaGastosFixos(GetFixedExpensesRequest requisicao)
        {
            return await  ObterGastosFixosBase(requisicao).SumAsync(x => x.ValorGastoFixo);
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
