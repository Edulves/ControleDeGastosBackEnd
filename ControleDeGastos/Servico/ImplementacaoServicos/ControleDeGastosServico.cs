using ControleDeGastos.Data.PadraoDeResposta.Base;
using ControleDeGastos.Data.ResultadoPaginado;
using ControleDeGastos.Data.ResultadoPaginado.Extensoes;
using ControleDeGastos.DTOs.Requisicao.GastosDiarios;
using ControleDeGastos.DTOs.Requisicoes.CategoriasRequisicoes;
using ControleDeGastos.DTOs.Requisicoes.ConsolidadoRequisicoes;
using ControleDeGastos.DTOs.Requisicoes.GastosFixosRequisicoes;
using ControleDeGastos.DTOs.Resposta.GastosDiarios;
using ControleDeGastos.DTOs.Respostas.ConsolidadoRespostas;
using ControleDeGastos.Modelos;
using ControleDeGastos.Repositorios.InterfaceRepositorios;
using ControleDeGastos.Servico.InterfaceServicos;

namespace ControleDeGastos.Servico.ImplementacaoServicos
{
    public class ControleDeGastosServico(IControleDeGastosRepositorio controleDeGastosRepositorio, IOperacoesGenericas operacoesGenericas) : IExpensesControlService
    {
        #region GastosDiarios
        public async Task<ResultPattern<string>> CreateDailyExpensesEntriesAsync(List<DailyExpenseEntryRequest> requisicao)
        {
            if (requisicao.Count <= 0)
                return ResultPattern<string>.Failure("Nenhuma dado foi enviado para cadastro!");

            var modeloBanco = requisicao.Select(x => new DailyExpenses
            {
                DataDoLancamento = x.DataDoLancamento,
                Valorgasto = x.Valorgasto,
                Observacao = x.Observacao,
                CategoriaId = x.CategoriaId,
                Deletado = "",
            }).ToList();

            await operacoesGenericas.CriarAsync(modeloBanco);

            return ResultPattern<string>.Success("Gasto cadrastrado com sucesso!");
        }
        public async Task<ResultPattern<PagedResult<DailyExpensesResult>>> GetDailyExpensesAsync(GetDailyExpensesRequest requisicao)
        {
            if(requisicao.InicioDoPeriodo > requisicao.FimDoPeriodo)
                return ResultPattern<PagedResult<DailyExpensesResult>>.Failure("Periodo de inicio não pode ser maior que o periodo de fim");

            if (requisicao.Page < 1)
                return ResultPattern<PagedResult<DailyExpensesResult>>.Failure("Pagina indicada não existe");

            var consulta = await controleDeGastosRepositorio.ObterGastosDiariosPaginado(requisicao);

            if (consulta.itens.Count <= 0)
                return ResultPattern<PagedResult<DailyExpensesResult>>.Failure("Nenhu registro de gastos encontrado");

            var resposta = consulta.itens.Select(x => new DailyExpensesResult
            {
                IdGastosDiario = x.IdGastosDiarios,
                DataDoLancamento = x.DataDoLancamento,
                Valorgasto = x.Valorgasto,
                Observacao = x.Observacao,
                NomeCategoria = x.categoria?.NomeDaCategoria ?? "",
            }).ToList();

            var respostaPaginada = (resposta, consulta.totalItens).ToPagedResult(requisicao.Page, requisicao.QTY);

            return ResultPattern<PagedResult<DailyExpensesResult>>.Success(respostaPaginada);
        }
        public async Task<ResultPattern<string>> UpdateDailyExpensesEntriesAsync(List<PutDailyExpensesRequest> requisicao)
        {
            if (requisicao.Count <= 0)
                return ResultPattern<string>.Failure($"Nenhum item para atualizar");

            var modeloBanco = new List<DailyExpenses>();

            foreach (var item in requisicao)
            {
                var consulta = await controleDeGastosRepositorio.ObterGastoDiarioPorId(item.IdGastosDiario);
                if (consulta == null)
                    return ResultPattern<string>.Failure($"Nenhum gasto diario de id: {item.IdGastosDiario} encontrado");

                consulta.DataDoLancamento = item.DataDoLancamento == DateTime.MinValue ? consulta.DataDoLancamento : item.DataDoLancamento;
                consulta.Valorgasto = item.Valorgasto <= 0 ? consulta.Valorgasto : item.Valorgasto;
                consulta.Observacao = string.IsNullOrEmpty(item.Observacao) ? consulta.Observacao : item.Observacao;
                consulta.CategoriaId = item.CategoriaId <= 0 ? consulta.CategoriaId : item.CategoriaId;

                modeloBanco.Add(consulta);
            }

            await operacoesGenericas.AtualizarAsync(modeloBanco);

            return ResultPattern<string>.Success("Itens atualizados com sucesso!");
        }
        public async Task<ResultPattern<string>> DeleteDailyExpenseEntryByIdAsync(int id)
        {
            var lancamentoParaFakeDelete = await controleDeGastosRepositorio.ObterGastoDiarioPorId(id);

            if(lancamentoParaFakeDelete == null)
                return ResultPattern<string>.Failure($"Não existe registro de id: {id}");

            lancamentoParaFakeDelete.Deletado = "*";

            await operacoesGenericas.AtualizarAsync(lancamentoParaFakeDelete);

            return ResultPattern<string>.Success("Item 'deletado' com sucesso!");
        }
        #endregion

        #region CategoriasDeGastos
        public async Task<ResultPattern<List<EntryCategories>>> GetEntryCategoriesAsync()
        {
            var consulta = await controleDeGastosRepositorio.ObterCategoriasDeLancamentos();

            return ResultPattern<List<EntryCategories>>.Success(consulta);
        }
        public async Task<ResultPattern<string>> CreateCategoriesAsync(List<CreateCategoryRequest> requisicao)
        {
            var novaCategoria = requisicao.Select(x => new EntryCategories()
            {
                NomeDaCategoria = x.NomeCategoria.ToLower(),
            }).ToList();

            await operacoesGenericas.CriarAsync(novaCategoria);

            return ResultPattern<string>.Success($"Categorias criadas com sucesso!");
        }
        public async Task<ResultPattern<string>> PutCategoriesAsync(List<EntryCategories> requisicao)
        {
            foreach (var item in requisicao)
            {
                item.NomeDaCategoria = item.NomeDaCategoria.ToLower();
            }
            
            await operacoesGenericas.AtualizarAsync(requisicao);

            return ResultPattern<string>.Success($"Categorias atualizadas com sucesso!");
        }
        public async Task<ResultPattern<string>> DeleteCategoryByIdAsync(int id)
        {
            var consulta = await controleDeGastosRepositorio.ObterCategoriasDeLancamentosPorId(id);

            if (consulta == null)
                return ResultPattern<string>.Failure($"Nenhuma categoria encontrada com id: {id}");

            consulta.Deletado = "*";

            await operacoesGenericas.AtualizarAsync(consulta);

            return ResultPattern<string>.Success($"Categoria deletada com sucesso!");
        }
        #endregion

        #region GastosFixos
        public async Task<ResultPattern<PagedResult<FixedExpenseResult>>> GetFixedExpensesAsync(GetFixedExpensesRequest requisicao)
        {
            if (requisicao.InicioDoPeriodo > requisicao.FimDoPeriodo)
                return ResultPattern<PagedResult<FixedExpenseResult>>.Failure("Periodo de inicio não pode ser maior que o periodo de fim");

            if (requisicao.Page < 1)
                return ResultPattern<PagedResult<FixedExpenseResult>>.Failure("Pagina indicada não existe");

            var consulta = await controleDeGastosRepositorio.ObterGastosFixos(requisicao);

            var respostaPaginada = (consulta.itens, consulta.totalItens).ToPagedResult(requisicao.Page, requisicao.QTY);

            return ResultPattern<PagedResult<FixedExpenseResult>>.Success(respostaPaginada);
        }
        public async Task<ResultPattern<string>> PostFixedExpenseAsync(List<PostFixedExpensesDto> requisicao)
        {

            var mapeamentoModelo = requisicao.Select(x => new FixedExpenseResult
            {
                DescricaoGastoFixo = x.DescricaoGastoFixo,
                ValorGastoFixo = x.ValorGastoFixo,
                DataDoLancamento = x.DataLancamento
            }).ToList();

            foreach (var item in mapeamentoModelo)
            {
                if (item.DataDoLancamento == DateTime.MinValue)
                    return ResultPattern<string>.Failure($"Invalid date {item.DataDoLancamento}", "Invalid date");
            }

            await operacoesGenericas.CriarAsync(mapeamentoModelo);

            return ResultPattern<string>.Success("Gastos fixo criados com sucesso!", StatusCodes.Status201Created);
        }
        public async Task<ResultPattern<string>> PutFixedExpensesAsync(List<PutFixedExpensesRequest> requisicao)
        {
            if(requisicao.Count <= 0)
                return ResultPattern<string>.Failure($"Nenhum item para atualizar");

            var modeloBanco = new List<FixedExpenseResult>();

            foreach (var item in requisicao)
            {
                var consulta = await controleDeGastosRepositorio.ObterGastosFixosPorId(item.IdGastosFixos);
                if (consulta == null){
                    modeloBanco.Add(new FixedExpenseResult()
                    {
                        DescricaoGastoFixo = item.DescricaoGastoFixo,
                        ValorGastoFixo = item.ValorGastoFixo,
                        DataDoLancamento = item.DataDoLancamento
                    });

                    continue;
                }

                consulta.DescricaoGastoFixo = string.IsNullOrEmpty(item.DescricaoGastoFixo) ? consulta.DescricaoGastoFixo : item.DescricaoGastoFixo;
                consulta.ValorGastoFixo = item.ValorGastoFixo <= 0 ? consulta.ValorGastoFixo : item.ValorGastoFixo;
                consulta.Pago = item.Pago ?? consulta.Pago;
                consulta.DataDoLancamento = item.DataDoLancamento == DateTime.MinValue ? consulta.DataDoLancamento : item.DataDoLancamento;

                modeloBanco.Add(consulta);
            }

            await operacoesGenericas.AtualizarAsync(modeloBanco);

            return ResultPattern<string>.Success("Gastos fixo atualizados com sucesso!");
        }
        public async Task<ResultPattern<string>> DeleteFixedExpensesAsync(int id)
        {
            var lancamentoParaFakeDelete = await controleDeGastosRepositorio.ObterGastosFixosPorId(id);

            if (lancamentoParaFakeDelete == null)
                return ResultPattern<string>.Failure($"Não existe registro de id: {id}");

            lancamentoParaFakeDelete.Deletado = "*";

            await operacoesGenericas.AtualizarAsync(lancamentoParaFakeDelete);

            return ResultPattern<string>.Success("Item 'deletado' com sucesso!");
        }
        #endregion

        #region Consolidado
        public async Task<ResultPattern<DailyExpensesPerCategoryResult>> GetExpensesSumPerCategoryAsync(GetByFullDateMothDayRequest requisicao)
        {
            var filtro = new GetDailyExpensesRequest()
            {
                InicioDoPeriodo = requisicao.BeginningOfPeriod,
                FimDoPeriodo = requisicao.EndOfPeriod,
                Ano = requisicao.Year,
                Mes = requisicao.Month
            };

            var consultaGastosDiarios = await controleDeGastosRepositorio.ObterGastosDiariosLista(filtro);
            
            if(consultaGastosDiarios.Count <= 0)
                return ResultPattern<DailyExpensesPerCategoryResult>.Failure("Nenhum gasto encontrado para os filtros ultilizados");

            var consultaAgrupada = consultaGastosDiarios.GroupBy(x => x.CategoriaId);

            var GastosPorCategoria = consultaAgrupada.Select(x => new ObterGastosDiariosConsolidadosPorCategoriasResposta()
            {
                NomeDaCategoria = x.First().categoria.NomeDaCategoria,
                ValorGasto = x.Sum(x => x.Valorgasto),
            }).OrderByDescending(x => x.ValorGasto).ToList();

            var resposta = new DailyExpensesPerCategoryResult();
            resposta.ListaDeGastosPorCategoria.AddRange(GastosPorCategoria);
            resposta.TotalDeGastos = resposta.ListaDeGastosPorCategoria.Sum(x => x.ValorGasto);

            return ResultPattern<DailyExpensesPerCategoryResult>.Success(resposta);
        }

        public async Task<ResultPattern<DailyExpensesConsolidationResult>> GetExpensesSumPerDayAsync(ExpensesByMothDayRequest requisicao)
        {
            var filtro = new GetDailyExpensesRequest() {
                Ano = requisicao.Year,
                Mes = requisicao.Month
            };

            var consultaGastosDiarios = await controleDeGastosRepositorio.ObterGastosDiariosLista(filtro);

            if (consultaGastosDiarios.Count <= 0)
                return ResultPattern<DailyExpensesConsolidationResult>.Failure("Nenhum gasto diario encontrado");

            var consultaAgrupada = consultaGastosDiarios.GroupBy(x => x.DataDoLancamento.Date);
           
            var GastosPorCategoria = consultaAgrupada.Select(x => new ObterGastosDiariosConsolidadosPorDiaResposta()
            {
                DataLancamento =  x.Key,
                ValorPorDia = x.Sum(x => x.Valorgasto)
            }).OrderBy(x => x.DataLancamento).ToList();

            var resposta = new DailyExpensesConsolidationResult();
            resposta.ListaDeGastosPorDia.AddRange(GastosPorCategoria);
            resposta.Total = resposta.ListaDeGastosPorDia.Sum(x => x.ValorPorDia);

            return ResultPattern<DailyExpensesConsolidationResult>.Success(resposta);
        }

        public async Task<ResultPattern<TotalFixedExpensesComparasionResult>> GetTotalFixedExpensesComparasionAsync(ExpensesByMothDayRequest requisicao)
        {
            var filtro = new GetFixedExpensesRequest()
            {
                Ano = requisicao.Year,
                Mes = requisicao.Month,
            };

            var consultaGastosFixos = await controleDeGastosRepositorio.ObterGastosFixosLista(filtro);

            if (consultaGastosFixos.Count <= 0)
                return ResultPattern<TotalFixedExpensesComparasionResult>.Failure("NenhumGastoFixoEncontrado");

            var resposta = new TotalFixedExpensesComparasionResult()
            {
                ValorPago = consultaGastosFixos.Where(x => x.Pago).Sum(x => x.ValorGastoFixo),
                ValorNaoPago = consultaGastosFixos.Where(x => !x.Pago).Sum(x => x.ValorGastoFixo),
            };

            return ResultPattern<TotalFixedExpensesComparasionResult>.Success(resposta);
        }

        public async Task<ResultPattern<TotalExpenses>> GetTotalDailyExpensesAsync(ExpensesByMothDayRequest requisicao)
        {
            var filtro = new GetDailyExpensesRequest()
            {
                Ano = requisicao.Year,
                Mes = requisicao.Month,
            };

            var somaGastosDiarios = await controleDeGastosRepositorio.ObterSomaGastosDiarios(filtro);

            var resposta = new TotalExpenses() { TotalGastos = somaGastosDiarios };

            return ResultPattern<TotalExpenses>.Success(resposta);
        }
        #endregion
    }
}
