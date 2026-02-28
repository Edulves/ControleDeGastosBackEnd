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
    public interface IControleDeGastosServico
    {
        #region GastosDiarios
        Task<RespostaPadrao<string>> CriarLancamentosDeGastosDiarios(List<CriarLancamentoDeGastoDiarioRequisicao> requisicao);
        Task<RespostaPadrao<ResultadoPaginado<ObterGastosDiariosResposta>>> ObterGastosDiarios(ObterGastosDiariosRequisicao requisicao);
        Task<RespostaPadrao<string>> AtualizarLancamentosDeGastosDiarios(List<AtualizarGastosDiariosRequisicao> requisicao);
        Task<RespostaPadrao<string>> FalsoDeleteLancamentosDeGastosDiarios(int id);
        #endregion

        #region CategoriasDeGastos
        Task<RespostaPadrao<List<CategoriasDeLancamentos>>> ObterCategoriasDeLancamentos();
        Task<RespostaPadrao<string>> CriarCategorias(List<CriarCategoriaRequisicao> requisicao);
        Task<RespostaPadrao<string>> AtualizarCategorias(List<CategoriasDeLancamentos> requisicao);
        Task<RespostaPadrao<string>> FalsoDeleteCategoria(int id);
        #endregion

        #region GastosFixos
        Task<RespostaPadrao<ResultadoPaginado<GastosFixos>>> ObterGastosFixos(ObterGastosFixosRequisicao requisicao);
        Task<RespostaPadrao<string>> CriarGastosFixos(List<CriarGastosFixosRequisicao> requisicao);
        Task<RespostaPadrao<string>> AtualizarGastosFixos(List<AtualizarGastosFixosRequisicao> requisicao);
        Task<RespostaPadrao<string>> FalsoDeleteGastosFixo(int id);
        #endregion

        #region Consolidado
        Task<RespostaPadrao<ObterGastosDiariosConsolidadosPorCategoriaComTotaisResposta>> ObterSomaDeGastoPorCategoria(GetByFullDateMothDayRequest requisicao);
        Task<RespostaPadrao<ObterGastosDiariosConsolidadosPorDiaComTotaisResposta>> ObterSomaDeGastoPorDia(GetByMothDayRequest requisicao);
        Task<RespostaPadrao<ObterGastosDiariosConsolidadosPagoVsNaoResposta>> ObterValorGastosFixosTotaisPagoVsNao(GetByMothDayRequest requisicao);
        Task<RespostaPadrao<ObterTotalDeGastos>> ObterTotalDeGastos(GetByMothDayRequest requisicao);
        #endregion
    }
}
