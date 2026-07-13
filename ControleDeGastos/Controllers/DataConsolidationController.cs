using ExpensesControl.Data.ResultPattern.Extensions;
using ExpensesControl.DTOs.Requests.DataConsolidationRequests;
using ExpensesControl.DTOs.Responses.DataConsolidationResponses;
using ExpensesControl.Service.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace ExpensesControl.Controllers
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
        public async Task<IActionResult> Get([FromQuery] GetByFullDateOrMothAndYearRequest request)
        {
            return (await _expensesControlService.GetExpensesSumPerCategoryAsync(request)).ToIActionResult(this);
        }

        [HttpGet("ExpensesPerDay")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<DailyExpensesConsolidationResult>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> GetExpensesPerDay([FromQuery] ExpensesByMothAndYearRequest request)
        {
            return (await _expensesControlService.GetExpensesSumPerDayAsync(request)).ToIActionResult(this);
        }

        [HttpGet("FixedExpenses")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<TotalFixedExpensesComparasionResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> GetFixedExpenses([FromQuery] ExpensesByMothAndYearRequest request)
        {
            return (await _expensesControlService.GetTotalFixedExpensesComparasionAsync(request)).ToIActionResult(this);
        }

        [HttpGet("TotalDailyExpenses")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<TotalExpensesResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> GetTotalDailyExpenses([FromQuery] ExpensesByMothAndYearRequest request)
        {
            return (await _expensesControlService.GetTotalDailyExpensesAsync(request)).ToIActionResult(this);
        }
    }
}
