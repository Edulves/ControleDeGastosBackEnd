using ControleDeGastos.DTOs.Requisicao.GastosDiarios;
using ControleDeGastos.Models;

namespace ControleDeGastos.Repositorios.InterfaceRepositorios
{
    public interface IControleDeGastosRepositorio
    {
        #region GastosDiarios
        Task<(List<GastosDiarios> itens, int totalItens)> ObterGastosDiarios(ObterGastosDiariosRequisicao obterGastosDiarios);
        Task<GastosDiarios?> ObterGastoDiarioPorId(int id);
        #endregion

        #region CategoriasDeGastos
        Task<List<CategoriasDeLancamentos>> ObterCategoriasDeLancamentos();
        Task<CategoriasDeLancamentos?> ObterCategoriasDeLancamentosPorId(int id);
        #endregion
    }
}
