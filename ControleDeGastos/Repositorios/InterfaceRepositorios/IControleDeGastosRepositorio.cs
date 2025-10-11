using ControleDeGastos.DTOs.Requisicao;
using ControleDeGastos.DTOs.Resposta;
using ControleDeGastos.Models;
using static ControleDeGastos.Data.PadraoDeResposta.Base.RespostaPadrao;

namespace ControleDeGastos.Repositorios.InterfaceRepositorios
{
    public interface IControleDeGastosRepositorio
    {
        #region GastosDiarios
        Task<(List<GastosDiarios> itens, int totalItens)> ObterGastosDiarios(ObterGastosDiariosRequisicao obterGastosDiarios);
        #endregion

        #region CategoriasDeGastos
        Task<List<CategoriasDeLancamentos>> ObterCategoriasDeLancamentos();
        #endregion
    }
}
