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
        Task<(List<GastosDiarios> itens, int totalItens)> ObterGastosDiariosPaginado(ObterGastosDiariosRequisicao obterGastosDiarios);
        Task<GastosDiarios?> ObterGastoDiarioPorId(int id);
        #endregion

        #region CategoriasDeGastos
        Task<List<CategoriasDeLancamentos>> ObterCategoriasDeLancamentos();
        Task<CategoriasDeLancamentos?> ObterCategoriasDeLancamentosPorId(int id);
        #endregion

        #region GastosFixos
        Task<(List<GastosFixos> itens, int totalItens)> ObterGastosFixos(ObterGastosFixosRequisicao requisicao);
        Task<GastosFixos?> ObterGastosFixosPorId(int id);
        #endregion

        #region Consolidado
        Task<List<GastosDiarios>> ObterGastosDiariosLista(ObterGastosDiariosConsolidadosPorCategoriaRequisicao requisicao);
        #endregion
    }
}
