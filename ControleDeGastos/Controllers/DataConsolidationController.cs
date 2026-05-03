using ControleDeGastos.Data.PadraoDeResposta.Extensao;
using ControleDeGastos.DTOs.Requisicoes.ConsolidadoRequisicoes;
using ControleDeGastos.DTOs.Respostas.ConsolidadoRespostas;
using ControleDeGastos.Servico.InterfaceServicos;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeGastos.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DataConsolidationController(IExpensesControlService expensesControlService) : ControllerBase
    {
        private readonly IExpensesControlService _expensesControlService = expensesControlService;

        [HttpGet("ExpensesPerCategory")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<DailyExpensesPerCategoryResult>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> Get([FromQuery] GetByFullDateMothDayRequest request)
        {
            return (await _expensesControlService.GetExpensesSumPerCategoryAsync(request)).ToIActionResult(this);
        }

        [HttpGet("ExpensesPerDay")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<DailyExpensesConsolidationResult>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> GetExpensesPerDay([FromQuery] ExpensesByMothDayRequest request)
        {
            return (await _expensesControlService.GetExpensesSumPerDayAsync(request)).ToIActionResult(this);
        }

        [HttpGet("FixedExpenses")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<TotalFixedExpensesComparasionResult>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> GetFixedExpenses([FromQuery] ExpensesByMothDayRequest request)
        {
            return (await _expensesControlService.GetTotalFixedExpensesComparasionAsync(request)).ToIActionResult(this);
        }

        [HttpGet("TotalDailyExpenses")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<TotalExpenses>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> GetTotalDailyExpenses([FromQuery] ExpensesByMothDayRequest request)
        {
            return (await _expensesControlService.GetTotalDailyExpensesAsync(request)).ToIActionResult(this);
        }
    }
}
