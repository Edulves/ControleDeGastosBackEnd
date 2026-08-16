using ExpensesControl.Data.ResultPattern.Extensions;
using ExpensesControl.DTOs.Requests.CategoriesRequests;
using ExpensesControl.Models;
using ExpensesControl.Service.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpensesControl.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class TransactionCategoriesController(ITransactionCategoriesService expensesControlService) : ControllerBase
    {
        private readonly ITransactionCategoriesService _expensesControlService = expensesControlService;
    
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<TransactionCategory>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> Get()
        {
            return (await _expensesControlService.GetEntryCategoriesAsync()).ToIActionResult(this);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<TransactionCategory>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> Post([FromBody] List<CreateCategoryRequest> request)
        {
            return (await _expensesControlService.CreateCategoriesAsync(request)).ToIActionResult(this);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<TransactionCategory>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> Put([FromBody] List<TransactionCategory> request)
        {
            return (await _expensesControlService.PutCategoriesAsync(request)).ToIActionResult(this);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<TransactionCategory>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            return (await _expensesControlService.DeleteCategoryByIdAsync(id)).ToIActionResult(this);
        }
    }
}
