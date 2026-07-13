using ExpensesControl.Data.PaginatedResult;
using ExpensesControl.Data.ResultPattern.Extensions;
using ExpensesControl.DTOs.Requests.DailyExpensesRequests;
using ExpensesControl.DTOs.Responses.DailyExpensesReponses;
using ExpensesControl.Service.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace ExpensesControl.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DailyExpensesController(IExpensesControlService expensesControlService) : ControllerBase
    {
        private readonly IExpensesControlService _expensesControlService = expensesControlService;

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<DailyExpensesResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> Get([FromQuery] GetDailyExpensesRequest request)
        {
            return (await _expensesControlService.GetDailyExpensesAsync(request)).ToIActionResult(this);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> Post([FromBody] List<DailyExpenseEntryRequest> request)
        {
            return (await _expensesControlService.CreateDailyExpensesEntriesAsync(request)).ToIActionResult(this);
        }

        [HttpPut]

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> Put([FromBody] List<PutDailyExpensesRequest> request)
        {
            return (await _expensesControlService.UpdateDailyExpensesEntriesAsync(request)).ToIActionResult(this);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            return (await _expensesControlService.DeleteDailyExpenseEntryByIdAsync(id)).ToIActionResult(this);
        }
    }
}
