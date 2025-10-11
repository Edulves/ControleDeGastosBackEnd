using ControleDeGastos.Data.ResultadoPaginado;
using ControleDeGastos.Data.ResultadoPaginado.Extensoes;
using ControleDeGastos.DTOs.Requisicao;
using ControleDeGastos.DTOs.Resposta;
using ControleDeGastos.Models;
using ControleDeGastos.Repositorios.InterfaceRepositorios;
using ControleDeGastos.Servico.InterfaceServicos;
using DocumentFormat.OpenXml.Office2016.Excel;
using static ControleDeGastos.Data.PadraoDeResposta.Base.RespostaPadrao;

namespace ControleDeGastos.Servico.ImplementacaoServicos
{
    public class ControleDeGastosServico(IControleDeGastosRepositorio controleDeGastosRepositorio) : IControleDeGastosServico
    {
        public async Task<Result<ResultadoPaginado<ObterGastosResposta>>> ObterGastosDiarios(ObterGastosDiarios obterGastosDiarios)
        {
            var consulta = await controleDeGastosRepositorio.ObterGastosDiarios(obterGastosDiarios);

            if (consulta.itens.Count <= 0)
                return Result<ResultadoPaginado<ObterGastosResposta>>.Failure("Nenhu registro de gastos encontrado");

            var resposta = consulta.itens.Select(x => new ObterGastosResposta
            {
                IdGastos = x.IdGastos,
                DataDoLancamento = x.DataDoLancamento,
                Valorgasto = x.Valorgasto,
                Observacao = x.Observacao,
                NomeCategoria = x.categoria.NomeDaCategoria,
            }).ToList();

            var respostaPaginada = (resposta, consulta.totalItens).ToPagedResult(obterGastosDiarios.Pagina, obterGastosDiarios.QtdPorPagina);

            return Result<ResultadoPaginado<ObterGastosResposta>>.Success(respostaPaginada);
        }

        public async Task<Result<List<CategoriasDeLancamentos>>> ObterCategoriasDeLancamentos()
        {
            var consulta = await controleDeGastosRepositorio.ObterCategoriasDeLancamentos();

            if (consulta.Count <= 0)
                return Result<List<CategoriasDeLancamentos>>.Failure("Nenhu registro de categoria de lançamento encontrado");

            return Result<List<CategoriasDeLancamentos>>.Success(consulta);
        }
    }
}
