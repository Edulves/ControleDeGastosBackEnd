using ControleDeGastos.Data.PadraoDeResposta.Base;
using ControleDeGastos.DTOs.Requisicao.GastosDiarios;
using ControleDeGastos.DTOs.Requisicoes.ConsolidadoRequisicoes;
using ControleDeGastos.DTOs.Requisicoes.GastosFixosRequisicoes;
using ControleDeGastos.Modelos;

namespace ControleDeGastos.Repositorios.InterfaceRepositorios
{
    public interface IControleDeGastosRepositorio
    {
        #region GastosDiarios
        Task<(List<DailyExpenses> itens, int totalItens)> ObterGastosDiariosPaginado(GetDailyExpensesRequest obterGastosDiarios);
        Task<List<DailyExpenses>> ObterGastosDiariosLista(GetDailyExpensesRequest requisicao);
        Task<DailyExpenses?> ObterGastoDiarioPorId(int id);
        Task<decimal> ObterSomaGastosDiarios(GetDailyExpensesRequest requisicao);
        #endregion

        #region CategoriasDeGastos
        Task<List<EntryCategories>> ObterCategoriasDeLancamentos();
        Task<EntryCategories?> ObterCategoriasDeLancamentosPorId(int id);
        #endregion

        #region GastosFixos
        Task<(List<FixedExpenseResult> itens, int totalItens)> ObterGastosFixos(GetFixedExpensesRequest requisicao);
        Task<List<FixedExpenseResult>> ObterGastosFixosLista(GetFixedExpensesRequest requisicao);
        Task<FixedExpenseResult?> ObterGastosFixosPorId(int id);
        Task<decimal> ObterSomaGastosFixos(GetFixedExpensesRequest requisicao);
        #endregion
    }
}
