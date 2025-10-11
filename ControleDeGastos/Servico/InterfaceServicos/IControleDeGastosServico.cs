using ControleDeGastos.Data.ResultadoPaginado;
using ControleDeGastos.DTOs.Requisicao;
using ControleDeGastos.DTOs.Resposta;
using ControleDeGastos.Models;
using static ControleDeGastos.Data.PadraoDeResposta.Base.RespostaPadrao;

namespace ControleDeGastos.Servico.InterfaceServicos
{
    public interface IControleDeGastosServico
    {
        Task<Result<ResultadoPaginado<ObterGastosResposta>>> ObterGastosDiarios(ObterGastosDiarios obterGastosDiarios);
        Task<Result<List<CategoriasDeLancamentos>>> ObterCategoriasDeLancamentos();
    }
}
