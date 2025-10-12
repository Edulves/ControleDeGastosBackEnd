using ControleDeGastos.Data.PadraoDeResposta.Base;
using ControleDeGastos.Data.ResultadoPaginado;
using ControleDeGastos.DTOs.Requisicao.GastosDiarios;
using ControleDeGastos.DTOs.Requisicoes.CategoriasRequisicoes;
using ControleDeGastos.DTOs.Resposta.GastosDiarios;
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
        Task<RespostaPadrao<string>> CriarCategorias(List<CriarCategoriaRequisicao> requisicao);
        Task<RespostaPadrao<string>> AtualizarCategorias(List<CategoriasDeLancamentos> requisicao);
        Task<RespostaPadrao<string>> FalsoDeleteCategoria(int id);
        #endregion
    }
}
