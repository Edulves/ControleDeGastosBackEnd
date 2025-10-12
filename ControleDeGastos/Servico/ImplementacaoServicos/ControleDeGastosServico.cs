using ControleDeGastos.Data.ResultadoPaginado;
using ControleDeGastos.Data.ResultadoPaginado.Extensoes;
using ControleDeGastos.Models;
using ControleDeGastos.Repositorios.InterfaceRepositorios;
using ControleDeGastos.Servico.InterfaceServicos;
using ControleDeGastos.Data.PadraoDeResposta.Base;
using ControleDeGastos.DTOs.Requisicao.GastosDiarios;
using ControleDeGastos.DTOs.Resposta.GastosDiarios;
using ControleDeGastos.DTOs.Requisicoes.CategoriasRequisicoes;
using ControleDeGastos.Repositorios.ImplementacaoRepositorios;

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
        public async Task<RespostaPadrao<string>> CriarCategorias(List<CriarCategoriaRequisicao> requisicao)
        {
            var novaCategoria = requisicao.Select(x => new CategoriasDeLancamentos()
            {
                NomeDaCategoria = x.NomeCategoria,
            }).ToList();

            await operacoesGenericas.CriarAsync(novaCategoria);

            return RespostaPadrao<string>.Success($"Categorias criadas com sucesso!");
        }
        public async Task<RespostaPadrao<string>> AtualizarCategorias(List<CategoriasDeLancamentos> requisicao)
        {
            await operacoesGenericas.AtualizarAsync(requisicao);

            return RespostaPadrao<string>.Success($"Categorias atualizadas com sucesso!");
        }
        public async Task<RespostaPadrao<string>> FalsoDeleteCategoria(int id)
        {
            var consulta = await controleDeGastosRepositorio.ObterCategoriasDeLancamentosPorId(id);

            if (consulta == null)
                return RespostaPadrao<string>.Failure($"Nenhuma categoria encontrada com id: {id}");

            consulta.Deletado = "*";

            await operacoesGenericas.AtualizarAsync(consulta);

            return RespostaPadrao<string>.Success($"Categoria deletada com sucesso!");
        }
        #endregion
    }
}
