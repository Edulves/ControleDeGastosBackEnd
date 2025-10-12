using ControleDeGastos.DTOs.Requisicao;
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
        #endregion
    }
}
