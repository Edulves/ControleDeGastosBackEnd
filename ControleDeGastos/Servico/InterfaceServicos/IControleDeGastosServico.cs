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
        Task<RespostaPadrao<string>> CriarLancamentosDeGastosDiarios(List<CriarLancamentoDeGastoDiarioRequisicao> requisicao);
        Task<RespostaPadrao<ResultadoPaginado<ObterGastosResposta>>> ObterGastosDiarios(ObterGastosDiariosRequisicao requisicao);
        Task<RespostaPadrao<string>> AtualizarLancamentosDeGastosDiarios(List<AtualizarGastosDiariosRequisicao> requisicao);
        Task<RespostaPadrao<string>> FalsoDeleteLancamentosDeGastosDiarios(int id);
        #endregion
        #region CategoriasDeGastos
        Task<RespostaPadrao<List<CategoriasDeLancamentos>>> ObterCategoriasDeLancamentos();
        #endregion
    }
}
