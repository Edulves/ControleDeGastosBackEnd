using ControleDeGastos.Data.PadraoDeResposta.Extensao;
using ControleDeGastos.DTOs.Requisicoes.GastosFixosRequisicoes;
using ControleDeGastos.Modelos;
using ControleDeGastos.Servico.InterfaceServicos;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeGastos.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FixedExpensesController(IExpenseControlService expenseControlService) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<FixedExpense>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> Get([FromQuery] GetFixedExpensesDto dto)
        {
            return (await expenseControlService.GetFixedExpensesAsync(dto)).ToIActionResult(this);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(List<FixedExpense>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> Post([FromBody] List<PostFixedExpensesDto> dto)
        {
            return (await expenseControlService.PostFixedExpenseAsync(dto)).ToIActionResult(this);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> Put([FromBody] List<AtualizarGastosFixosRequisicao> dto)
        {
            return (await expenseControlService.PutFixedExpensesAsync(dto)).ToIActionResult(this);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            return (await expenseControlService.DeleteFixedExpensesAsync(id)).ToIActionResult(this);
        }
    }
}
