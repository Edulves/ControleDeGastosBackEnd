using ControleDeGastos.Data.ResultadoPaginado;
using ControleDeGastos.Data.ResultadoPaginado.Extensoes;
using ControleDeGastos.DTOs.Requisicao;
using ControleDeGastos.DTOs.Resposta;
using ControleDeGastos.Models;
using ControleDeGastos.Repositorios.InterfaceRepositorios;
using ControleDeGastos.Servico.InterfaceServicos;
using ControleDeGastos.Data.PadraoDeResposta.Base;

namespace ControleDeGastos.Servico.ImplementacaoServicos
{
    public class ControleDeGastosServico(IControleDeGastosRepositorio controleDeGastosRepositorio, IOperacoesGenericas operacoesGenericas) : IControleDeGastosServico
    {
        #region GastosDiarios
        public async Task<RespostaPadrao<string>> CriarLancamentosDeGastosDiarios(List<CriarLancamentoDeGastoDiarioRequisicao> requisicao)
        {
            var modeloBanco = requisicao.Select(x => new GastosDiarios
            {
                DataDoLancamento = x.DataDoLancamento,
                Valorgasto = x.Valorgasto,
                Observacao = x.Observacao,
                CategoriaId = x.CategoriaId,
                Deletado = "",
            }).ToList();

            await operacoesGenericas.CriarAsync(modeloBanco);

            return RespostaPadrao<string>.Success("Gasto cadrastrado com sucesso!");
        }

        public async Task<RespostaPadrao<ResultadoPaginado<ObterGastosResposta>>> ObterGastosDiarios(ObterGastosDiariosRequisicao obterGastosDiarios)
        {
            if (obterGastosDiarios.Pagina < 1)
                return RespostaPadrao<ResultadoPaginado<ObterGastosResposta>>.Failure("Pagina indicada não existe");

            var consulta = await controleDeGastosRepositorio.ObterGastosDiarios(obterGastosDiarios);

            if (consulta.itens.Count <= 0)
                return RespostaPadrao<ResultadoPaginado<ObterGastosResposta>>.Failure("Nenhu registro de gastos encontrado");

            var resposta = consulta.itens.Select(x => new ObterGastosResposta
            {
                IdGastos = x.IdGastos,
                DataDoLancamento = x.DataDoLancamento,
                Valorgasto = x.Valorgasto,
                Observacao = x.Observacao,
                NomeCategoria = x.categoria?.NomeDaCategoria ?? "",
            }).ToList();

            var respostaPaginada = (resposta, consulta.totalItens).ToPagedResult(obterGastosDiarios.Pagina, obterGastosDiarios.QtdPorPagina);

            return RespostaPadrao<ResultadoPaginado<ObterGastosResposta>>.Success(respostaPaginada);
        }

        public async Task<RespostaPadrao<string>> AtualizarLancamentosDeGastosDiarios(List<AtualizarGastosDiariosRequisicao> requisicao)
        {
            var modeloBanco = requisicao.Select(x => new GastosDiarios
            {
                IdGastos = x.IdGastos,
                DataDoLancamento = x.DataDoLancamento,
                Valorgasto = x.Valorgasto,
                Observacao = x.Observacao,
                CategoriaId = x.CategoriaId,
                Deletado = "",
            }).ToList();

            await operacoesGenericas.AtualizarAsync(modeloBanco);

            return RespostaPadrao<string>.Success("Itens atualizados com sucesso!");
        }

        public async Task<RespostaPadrao<string>> FalsoDeleteLancamentosDeGastosDiarios(int id)
        {
            var lancamentoParaFakeDelete = await controleDeGastosRepositorio.ObterGastoDiarioPorId(id);

            if(lancamentoParaFakeDelete == null)
                return RespostaPadrao<string>.Failure($"Não existe registro de id: {id}");

            lancamentoParaFakeDelete.Deletado = "*";

            await operacoesGenericas.AtualizarAsync(lancamentoParaFakeDelete);

            return RespostaPadrao<string>.Success("Item 'deletado' com sucesso!");
        }
        #endregion

        #region CategoriasDeGastos
        public async Task<RespostaPadrao<List<CategoriasDeLancamentos>>> ObterCategoriasDeLancamentos()
        {
            var consulta = await controleDeGastosRepositorio.ObterCategoriasDeLancamentos();

            if (consulta.Count <= 0)
                return RespostaPadrao<List<CategoriasDeLancamentos>>.Failure("Nenhu registro de categoria de lançamento encontrado");

            return RespostaPadrao<List<CategoriasDeLancamentos>>.Success(consulta);
        }
        #endregion
    }
}
