using ExpensesControl.Data.ResultPattern.Extensions;
using ExpensesControl.DTOs.Requests.FixedExpensesRequests;
using ExpensesControl.Models;
using ExpensesControl.Service.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace ExpensesControl.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FixedExpensesController(IFixedExpensesService expensesControlService) : ControllerBase
    {
        private readonly IFixedExpensesService _expensesControlService = expensesControlService;

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<FixedExpense>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> Get([FromQuery] GetFixedExpensesRequest request)
        {
            return (await _expensesControlService.GetFixedExpensesAsync(request)).ToIActionResult(this);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(List<FixedExpense>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> Post([FromBody] List<PostFixedExpensesDto> request)
        {
            return (await _expensesControlService.PostFixedExpenseAsync(request)).ToIActionResult(this);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> Put([FromBody] List<PutFixedExpensesRequest> request)
        {
            return (await _expensesControlService.PutFixedExpensesAsync(request)).ToIActionResult(this);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            return (await _expensesControlService.DeleteFixedExpensesAsync(id)).ToIActionResult(this);
        }
    }
}
