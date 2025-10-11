using ControleDeGastos.Data.PadraoDeResposta.Base;
using ControleDeGastos.Data.ResultadoPaginado;
using ControleDeGastos.DTOs.Requisicao;
using ControleDeGastos.DTOs.Resposta;
using ControleDeGastos.Models;

namespace ControleDeGastos.Servico.InterfaceServicos
{
    public interface IControleDeGastosServico
    {
        #region GastosDiarios
        Task<RespostaPadrao<List<GastosDiarios>>> CriarLancamentosDeGastosDiarios(List<CriarLancamentoDeGastoDiarioRequisicao> requisicao);
        Task<RespostaPadrao<ResultadoPaginado<ObterGastosResposta>>> ObterGastosDiarios(ObterGastosDiariosRequisicao requisicao);
        #endregion
        #region CategoriasDeGastos
        Task<RespostaPadrao<List<CategoriasDeLancamentos>>> ObterCategoriasDeLancamentos();
        #endregion
    }
}
