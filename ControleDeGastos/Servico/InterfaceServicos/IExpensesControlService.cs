using ControleDeGastos.Data.PadraoDeResposta.Base;
using ControleDeGastos.Data.ResultadoPaginado;
using ControleDeGastos.DTOs.Requisicao.GastosDiarios;
using ControleDeGastos.DTOs.Requisicoes.CategoriasRequisicoes;
using ControleDeGastos.DTOs.Requisicoes.ConsolidadoRequisicoes;
using ControleDeGastos.DTOs.Requisicoes.GastosFixosRequisicoes;
using ControleDeGastos.DTOs.Resposta.GastosDiarios;
using ControleDeGastos.DTOs.Respostas.ConsolidadoRespostas;
using ControleDeGastos.Modelos;

namespace ControleDeGastos.Servico.InterfaceServicos
{
    public interface IExpensesControlService
    {
        #region GastosDiarios
        Task<ResultPattern<string>> CreateDailyExpensesEntriesAsync(List<DailyExpenseEntryRequest> requisicao);
        Task<ResultPattern<PagedResult<DailyExpensesResult>>> GetDailyExpensesAsync(GetDailyExpensesRequest requisicao);
        Task<ResultPattern<string>> UpdateDailyExpensesEntriesAsync(List<PutDailyExpensesRequest> requisicao);
        Task<ResultPattern<string>> DeleteDailyExpenseEntryByIdAsync(int id);
        #endregion

        #region CategoriasDeGastos
        Task<ResultPattern<List<EntryCategories>>> GetEntryCategoriesAsync();
        Task<ResultPattern<string>> CreateCategoriesAsync(List<CreateCategoryRequest> requisicao);
        Task<ResultPattern<string>> PutCategoriesAsync(List<EntryCategories> requisicao);
        Task<ResultPattern<string>> DeleteCategoryByIdAsync(int id);
        #endregion

        #region GastosFixos
        Task<ResultPattern<PagedResult<FixedExpenseResult>>> GetFixedExpensesAsync(GetFixedExpensesRequest requisicao);
        Task<ResultPattern<string>> PostFixedExpenseAsync(List<PostFixedExpensesDto> requisicao);
        Task<ResultPattern<string>> PutFixedExpensesAsync(List<PutFixedExpensesRequest> requisicao);
        Task<ResultPattern<string>> DeleteFixedExpensesAsync(int id);
        #endregion

        #region Consolidado
        Task<ResultPattern<DailyExpensesPerCategoryResult>> GetExpensesSumPerCategoryAsync(GetByFullDateMothDayRequest requisicao);
        Task<ResultPattern<DailyExpensesConsolidationResult>> GetExpensesSumPerDayAsync(ExpensesByMothDayRequest requisicao);
        Task<ResultPattern<TotalFixedExpensesComparasionResult>> GetTotalFixedExpensesComparasionAsync(ExpensesByMothDayRequest requisicao);
        Task<ResultPattern<TotalExpenses>> GetTotalDailyExpensesAsync(ExpensesByMothDayRequest requisicao);
        #endregion
    }
}
