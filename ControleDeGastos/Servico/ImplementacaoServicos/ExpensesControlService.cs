using ExpensesControl.Data.PaginatedResult;
using ExpensesControl.Data.PaginatedResult.Extentions;
using ExpensesControl.Data.ResultPattern.Base;
using ExpensesControl.DTOs.Requests.CategoriesRequests;
using ExpensesControl.DTOs.Requests.DailyExpensesRequests;
using ExpensesControl.DTOs.Requests.DataConsolidationRequests;
using ExpensesControl.DTOs.Requests.FixedExpensesRequests;
using ExpensesControl.DTOs.Requisicoes.GastosFixosRequisicoes;
using ExpensesControl.DTOs.Responses.DailyExpensesReponses;
using ExpensesControl.DTOs.Responses.DataConsolidationResponses;
using ExpensesControl.Models;
using ExpensesControl.Repositories.RepositoriesInterface;
using ExpensesControl.Service.ServiceInterfaces;

namespace ExpensesControl.Service.ServiceImplementations
{
    public class ExpensesControlService(IExpensesControlRepositories controleDeGastosRepositorio, IGenericOperations operacoesGenericas) : IExpensesControlService
    {
        #region GastosDiarios
        public async Task<ResultPattern<string>> CreateDailyExpensesEntriesAsync(List<DailyExpenseEntryRequest> requisicao)
        {
            if (requisicao.Count <= 0)
                return ResultPattern<string>.Failure("Nenhuma dado foi enviado para cadastro!");

            var modeloBanco = requisicao.Select(x => new DailyExpenses
            {
                InputDate = x.InputDate,
                ExpenseValue = x.ExpenseValue,
                Note = x.Observacao,
                CategoryId = x.CategoryId,
                Deleted = "",
            }).ToList();

            await operacoesGenericas.CriarAsync(modeloBanco);

            return ResultPattern<string>.Success("Gasto cadrastrado com sucesso!");
        }
        public async Task<ResultPattern<PagedResult<DailyExpensesResponse>>> GetDailyExpensesAsync(GetDailyExpensesRequest requisicao)
        {
            if(requisicao.BeginningOfPeriod > requisicao.FimDoPeriodo)
                return ResultPattern<PagedResult<DailyExpensesResponse>>.Failure("Periodo de inicio não pode ser maior que o periodo de fim");

            if (requisicao.Page < 1)
                return ResultPattern<PagedResult<DailyExpensesResponse>>.Failure("Pagina indicada não existe");

            var consulta = await controleDeGastosRepositorio.ObterGastosDiariosPaginado(requisicao);

            if (consulta.itens.Count <= 0)
                return ResultPattern<PagedResult<DailyExpensesResponse>>.Failure("Nenhu registro de gastos encontrado");

            var resposta = consulta.itens.Select(x => new DailyExpensesResponse
            {
                DailyExpenseId = x.DailyExpensesId,
                InputDate = x.InputDate,
                ExpenseValue = x.ExpenseValue,
                Note = x.Note,
                CategoryName = x.Category?.CategoryName ?? "",
            }).ToList();

            var respostaPaginada = (resposta, consulta.totalItens).ToPagedResult(requisicao.Page, requisicao.QTY);

            return ResultPattern<PagedResult<DailyExpensesResponse>>.Success(respostaPaginada);
        }
        public async Task<ResultPattern<string>> UpdateDailyExpensesEntriesAsync(List<PutDailyExpensesRequest> requisicao)
        {
            if (requisicao.Count <= 0)
                return ResultPattern<string>.Failure($"Nenhum item para atualizar");

            var modeloBanco = new List<DailyExpenses>();

            foreach (var item in requisicao)
            {
                var consulta = await controleDeGastosRepositorio.ObterGastoDiarioPorId(item.DailyExpenseId);
                if (consulta == null)
                    return ResultPattern<string>.Failure($"Nenhum gasto diario de id: {item.DailyExpenseId} encontrado");

                consulta.InputDate = item.InputDate == DateTime.MinValue ? consulta.InputDate : item.InputDate;
                consulta.ExpenseValue = item.ExpenseValue <= 0 ? consulta.ExpenseValue : item.ExpenseValue;
                consulta.Note = string.IsNullOrEmpty(item.Note) ? consulta.Note : item.Note;
                consulta.CategoryId = item.CategoryId <= 0 ? consulta.CategoryId : item.CategoryId;

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

            lancamentoParaFakeDelete.Deleted = "*";

            await operacoesGenericas.AtualizarAsync(lancamentoParaFakeDelete);

            return ResultPattern<string>.Success("Item 'deletado' com sucesso!");
        }
        #endregion

        #region CategoriasDeGastos
        public async Task<ResultPattern<List<TransactionCategories>>> GetEntryCategoriesAsync()
        {
            var consulta = await controleDeGastosRepositorio.ObterCategoriasDeLancamentos();

            return ResultPattern<List<TransactionCategories>>.Success(consulta);
        }
        public async Task<ResultPattern<string>> CreateCategoriesAsync(List<CreateCategoryRequest> requisicao)
        {
            var novaCategoria = requisicao.Select(x => new TransactionCategories()
            {
                CategoryName = x.CategoryName.ToLower(),
            }).ToList();

            await operacoesGenericas.CriarAsync(novaCategoria);

            return ResultPattern<string>.Success($"Categorias criadas com sucesso!");
        }
        public async Task<ResultPattern<string>> PutCategoriesAsync(List<TransactionCategories> requisicao)
        {
            foreach (var item in requisicao)
            {
                item.CategoryName = item.CategoryName.ToLower();
            }
            
            await operacoesGenericas.AtualizarAsync(requisicao);

            return ResultPattern<string>.Success($"Categorias atualizadas com sucesso!");
        }
        public async Task<ResultPattern<string>> DeleteCategoryByIdAsync(int id)
        {
            var consulta = await controleDeGastosRepositorio.ObterCategoriasDeLancamentosPorId(id);

            if (consulta == null)
                return ResultPattern<string>.Failure($"Nenhuma categoria encontrada com id: {id}");

            consulta.Deleted = "*";

            await operacoesGenericas.AtualizarAsync(consulta);

            return ResultPattern<string>.Success($"Categoria deletada com sucesso!");
        }
        #endregion

        #region GastosFixos
        public async Task<ResultPattern<PagedResult<FixedExpenseResult>>> GetFixedExpensesAsync(GetFixedExpensesRequest requisicao)
        {
            if (requisicao.BeginningOfPeriod > requisicao.EndOfPeriod)
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
                FixedExpenseDescription = x.FixedExpenseDescription,
                FixedExpenseValue = x.FixedExpenseValue,
                InputDate = x.InputDate
            }).ToList();

            foreach (var item in mapeamentoModelo)
            {
                if (item.InputDate == DateTime.MinValue)
                    return ResultPattern<string>.Failure($"Invalid date {item.InputDate}", "Invalid date");
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
                var consulta = await controleDeGastosRepositorio.ObterGastosFixosPorId(item.FixedExpensesId);
                if (consulta == null){
                    modeloBanco.Add(new FixedExpenseResult()
                    {
                        FixedExpenseDescription = item.FixedExpenseDescription,
                        FixedExpenseValue = item.FixedExpenseValue,
                        InputDate = item.InputDate
                    });

                    continue;
                }

                consulta.FixedExpenseDescription = string.IsNullOrEmpty(item.FixedExpenseDescription) ? consulta.FixedExpenseDescription : item.FixedExpenseDescription;
                consulta.FixedExpenseValue = item.FixedExpenseValue <= 0 ? consulta.FixedExpenseValue : item.FixedExpenseValue;
                consulta.Paid = item.Paid ?? consulta.Paid;
                consulta.InputDate = item.InputDate == DateTime.MinValue ? consulta.InputDate : item.InputDate;

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

            lancamentoParaFakeDelete.Deleted = "*";

            await operacoesGenericas.AtualizarAsync(lancamentoParaFakeDelete);

            return ResultPattern<string>.Success("Item 'deletado' com sucesso!");
        }
        #endregion

        #region Consolidado
        public async Task<ResultPattern<DailyExpensesPerCategoryResult>> GetExpensesSumPerCategoryAsync(GetByFullDateOrMothAndYearRequest requisicao)
        {
            var filtro = new GetDailyExpensesRequest()
            {
                BeginningOfPeriod = requisicao.BeginningOfPeriod,
                FimDoPeriodo = requisicao.EndOfPeriod,
                Year = requisicao.Year,
                Month = requisicao.Month
            };

            var consultaGastosDiarios = await controleDeGastosRepositorio.ObterGastosDiariosLista(filtro);
            
            if(consultaGastosDiarios.Count <= 0)
                return ResultPattern<DailyExpensesPerCategoryResult>.Failure("Nenhum gasto encontrado para os filtros ultilizados");

            var consultaAgrupada = consultaGastosDiarios.GroupBy(x => x.CategoryId);

            var GastosPorCategoria = consultaAgrupada.Select(x => new GetDailyExpensesByCategoryReponse()
            {
                CategoryName = x.First().Category.CategoryName,
                ExpenseValue = x.Sum(x => x.ExpenseValue),
            }).OrderByDescending(x => x.ExpenseValue).ToList();

            var resposta = new DailyExpensesPerCategoryResult();
            resposta.DailyExpensesByCategoryList.AddRange(GastosPorCategoria);
            resposta.Total = resposta.DailyExpensesByCategoryList.Sum(x => x.ExpenseValue);

            return ResultPattern<DailyExpensesPerCategoryResult>.Success(resposta);
        }

        public async Task<ResultPattern<DailyExpensesConsolidationResult>> GetExpensesSumPerDayAsync(ExpensesByMothAndYearRequest requisicao)
        {
            var filtro = new GetDailyExpensesRequest() {
                Year = requisicao.Year,
                Month = requisicao.Month
            };

            var consultaGastosDiarios = await controleDeGastosRepositorio.ObterGastosDiariosLista(filtro);

            if (consultaGastosDiarios.Count <= 0)
                return ResultPattern<DailyExpensesConsolidationResult>.Failure("Nenhum gasto diario encontrado");

            var consultaAgrupada = consultaGastosDiarios.GroupBy(x => x.InputDate.Date);
           
            var GastosPorCategoria = consultaAgrupada.Select(x => new GetDailyExpensesByDayResponse()
            {
                InputDate =  x.Key,
                ExpenseValuePerDay = x.Sum(x => x.ExpenseValue)
            }).OrderBy(x => x.InputDate).ToList();

            var resposta = new DailyExpensesConsolidationResult();
            resposta.DailyExpensesList.AddRange(GastosPorCategoria);
            resposta.Total = resposta.DailyExpensesList.Sum(x => x.ExpenseValuePerDay);

            return ResultPattern<DailyExpensesConsolidationResult>.Success(resposta);
        }

        public async Task<ResultPattern<TotalFixedExpensesComparasionResponse>> GetTotalFixedExpensesComparasionAsync(ExpensesByMothAndYearRequest requisicao)
        {
            var filtro = new GetFixedExpensesRequest()
            {
                Year = requisicao.Year,
                Month = requisicao.Month,
            };

            var consultaGastosFixos = await controleDeGastosRepositorio.ObterGastosFixosLista(filtro);

            if (consultaGastosFixos.Count <= 0)
                return ResultPattern<TotalFixedExpensesComparasionResponse>.Failure("NenhumGastoFixoEncontrado");

            var resposta = new TotalFixedExpensesComparasionResponse()
            {
                PaidValue = consultaGastosFixos.Where(x => x.Paid).Sum(x => x.FixedExpenseValue),
                NotPaidValue = consultaGastosFixos.Where(x => !x.Paid).Sum(x => x.FixedExpenseValue),
            };

            return ResultPattern<TotalFixedExpensesComparasionResponse>.Success(resposta);
        }

        public async Task<ResultPattern<TotalExpensesResponse>> GetTotalDailyExpensesAsync(ExpensesByMothAndYearRequest requisicao)
        {
            var filtro = new GetDailyExpensesRequest()
            {
                Year = requisicao.Year,
                Month = requisicao.Month,
            };

            var somaGastosDiarios = await controleDeGastosRepositorio.ObterSomaGastosDiarios(filtro);

            var resposta = new TotalExpensesResponse() { TotalExpenses = somaGastosDiarios };

            return ResultPattern<TotalExpensesResponse>.Success(resposta);
        }
        #endregion
    }
}
