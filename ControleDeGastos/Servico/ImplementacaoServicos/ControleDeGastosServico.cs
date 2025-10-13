using ControleDeGastos.Data.PadraoDeResposta.Base;
using ControleDeGastos.Data.ResultadoPaginado;
using ControleDeGastos.Data.ResultadoPaginado.Extensoes;
using ControleDeGastos.DTOs.Requisicao.GastosDiarios;
using ControleDeGastos.DTOs.Requisicoes.CategoriasRequisicoes;
using ControleDeGastos.DTOs.Requisicoes.GastosFixosRequisicoes;
using ControleDeGastos.DTOs.Resposta.GastosDiarios;
using ControleDeGastos.Modelos;
using ControleDeGastos.Repositorios.InterfaceRepositorios;
using ControleDeGastos.Servico.InterfaceServicos;

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
        public async Task<RespostaPadrao<ResultadoPaginado<ObterGastosDiariosResposta>>> ObterGastosDiarios(ObterGastosDiariosRequisicao requisicao)
        {
            if(requisicao.InicioDoPeriodo > requisicao.FimDoPeriodo)
                return RespostaPadrao<ResultadoPaginado<ObterGastosDiariosResposta>>.Failure("Periodo de inicio não pode ser maior que o periodo de fim");

            if (requisicao.Pagina < 1)
                return RespostaPadrao<ResultadoPaginado<ObterGastosDiariosResposta>>.Failure("Pagina indicada não existe");

            var consulta = await controleDeGastosRepositorio.ObterGastosDiarios(requisicao);

            if (consulta.itens.Count <= 0)
                return RespostaPadrao<ResultadoPaginado<ObterGastosDiariosResposta>>.Failure("Nenhu registro de gastos encontrado");

            var resposta = consulta.itens.Select(x => new ObterGastosDiariosResposta
            {
                IdGastosDiario = x.IdGastosDiarios,
                DataDoLancamento = x.DataDoLancamento,
                Valorgasto = x.Valorgasto,
                Observacao = x.Observacao,
                NomeCategoria = x.categoria?.NomeDaCategoria ?? "",
            }).ToList();

            var respostaPaginada = (resposta, consulta.totalItens).ToPagedResult(requisicao.Pagina, requisicao.QtdPorPagina);

            return RespostaPadrao<ResultadoPaginado<ObterGastosDiariosResposta>>.Success(respostaPaginada);
        }
        public async Task<RespostaPadrao<string>> AtualizarLancamentosDeGastosDiarios(List<AtualizarGastosDiariosRequisicao> requisicao)
        {
            if (requisicao.Count <= 0)
                return RespostaPadrao<string>.Failure($"Nenhum item para atualizar");

            var modeloBanco = new List<GastosDiarios>();

            foreach (var item in requisicao)
            {
                var consulta = await controleDeGastosRepositorio.ObterGastoDiarioPorId(item.IdGastosDiario);
                if (consulta == null)
                    return RespostaPadrao<string>.Failure($"Nenhum gasto diario de id: {item.IdGastosDiario} encontrado");

                consulta.DataDoLancamento = item.DataDoLancamento == DateTime.MinValue ? consulta.DataDoLancamento : item.DataDoLancamento;
                consulta.Valorgasto = item.Valorgasto <= 0 ? consulta.Valorgasto : item.Valorgasto;
                consulta.Observacao = string.IsNullOrEmpty(item.Observacao) ? consulta.Observacao : item.Observacao;
                consulta.CategoriaId = item.CategoriaId <= 0 ? consulta.CategoriaId : item.CategoriaId;

                modeloBanco.Add(consulta);
            }

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
                NomeDaCategoria = x.NomeCategoria.ToLower(),
            }).ToList();

            await operacoesGenericas.CriarAsync(novaCategoria);

            return RespostaPadrao<string>.Success($"Categorias criadas com sucesso!");
        }
        public async Task<RespostaPadrao<string>> AtualizarCategorias(List<CategoriasDeLancamentos> requisicao)
        {
            foreach (var item in requisicao)
            {
                item.NomeDaCategoria = item.NomeDaCategoria.ToLower();
            }
            
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

        #region GastosFixos
        public async Task<RespostaPadrao<ResultadoPaginado<GastosFixos>>> ObterGastosFixos(ObterGastosFixosRequisicao requisicao)
        {
            if (requisicao.InicioDoPeriodo > requisicao.FimDoPeriodo)
                return RespostaPadrao<ResultadoPaginado<GastosFixos>>.Failure("Periodo de inicio não pode ser maior que o periodo de fim");

            if (requisicao.Pagina < 1)
                return RespostaPadrao<ResultadoPaginado<GastosFixos>>.Failure("Pagina indicada não existe");

            var consulta = await controleDeGastosRepositorio.ObterGastosFixos(requisicao);

            var respostaPaginada = (consulta.itens, consulta.totalItens).ToPagedResult(requisicao.Pagina, requisicao.QtdPorPagina);

            return RespostaPadrao<ResultadoPaginado<GastosFixos>>.Success(respostaPaginada);
        }
        public async Task<RespostaPadrao<string>> CriarGastosFixos(List<CriarGastosFixosRequisicao> requisicao)
        {

            var mapeamentoModelo = requisicao.Select(x => new GastosFixos
            {
                DescricaoGastoFixo = x.DescricaoGastoFixo,
                ValorGastoFixo = x.ValorGastoFixo,
                DataDoLancamento = x.DataLancamento
            }).ToList();

            await operacoesGenericas.CriarAsync(mapeamentoModelo);

            return RespostaPadrao<string>.Success("Gastos fixo criados com sucesso!");
        }
        public async Task<RespostaPadrao<string>> AtualizarGastosFixos(List<AtualizarGastosFixosRequisicao> requisicao)
        {
            if(requisicao.Count <= 0)
                return RespostaPadrao<string>.Failure($"Nenhum item para atualizar");

            var modeloBanco = new List<GastosFixos>();

            foreach (var item in requisicao)
            {
                var consulta = await controleDeGastosRepositorio.ObterGastosFixosPorId(item.IdGastosFixos);
                if (consulta == null)
                    return RespostaPadrao<string>.Failure($"Nenhum gasto fixo de id: {item.IdGastosFixos} encontrado");

                consulta.DescricaoGastoFixo = string.IsNullOrEmpty(item.DescricaoGastoFixo) ? consulta.DescricaoGastoFixo : item.DescricaoGastoFixo;
                consulta.ValorGastoFixo = item.ValorGastoFixo <= 0 ? consulta.ValorGastoFixo : item.ValorGastoFixo;
                consulta.Pago = item.Pago ?? consulta.Pago;
                consulta.DataDoLancamento = item.DataDoLancamento == DateTime.MinValue ? consulta.DataDoLancamento : item.DataDoLancamento;

                modeloBanco.Add(consulta);
            }

            await operacoesGenericas.AtualizarAsync(modeloBanco);

            return RespostaPadrao<string>.Success("Gastos fixo atualizados com sucesso!");
        }
        public async Task<RespostaPadrao<string>> FalsoDeleteGastosFixo(int id)
        {
            var lancamentoParaFakeDelete = await controleDeGastosRepositorio.ObterGastosFixosPorId(id);

            if (lancamentoParaFakeDelete == null)
                return RespostaPadrao<string>.Failure($"Não existe registro de id: {id}");

            lancamentoParaFakeDelete.Deletado = "*";

            await operacoesGenericas.AtualizarAsync(lancamentoParaFakeDelete);

            return RespostaPadrao<string>.Success("Item 'deletado' com sucesso!");
        }
        #endregion
    }
}
