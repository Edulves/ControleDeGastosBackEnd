using ControleDeGastos.Data.PadraoDeResposta.Extensao;
using ControleDeGastos.DTOs.Erros;
using ControleDeGastos.DTOs.Requisicoes.ConsolidadoRequisicoes;
using ControleDeGastos.DTOs.Respostas.ConsolidadoRespostas;
using ControleDeGastos.Servico.InterfaceServicos;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeGastos.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DataConsolidationController(IControleDeGastosServico controleDeGastosServico) : ControllerBase
    {
        [HttpGet("GetExpensesPerCategory")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ObterGastosDiariosConsolidadosPorCategoriaComTotaisResposta>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(DetalhesDeProblemas))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(DetalhesDeProblemas))]
        public async Task<IActionResult> GetExpensesPerCategory([FromQuery] GetByFullDateMothDayRequest requisicao)
        {
            try
            {
                return (await controleDeGastosServico.ObterSomaDeGastoPorCategoria(requisicao)).ToIActionResult(this);
            }
            catch (Exception ex)
            {
                var problem = new DetalhesDeProblemas
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Titulo = "Erro interno no servidor",
                    Detalhe = ex.Message,
                    Instancia = HttpContext.Request.Path
                };

                return StatusCode(StatusCodes.Status500InternalServerError, problem);
            }
        }

        [HttpGet("GetExpensesPerDay")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ObterGastosDiariosConsolidadosPorDiaComTotaisResposta>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(DetalhesDeProblemas))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(DetalhesDeProblemas))]
        public async Task<IActionResult> GetExpensesPerDay([FromQuery] GetByMothDayRequest requisicao)
        {
            try
            {
                return (await controleDeGastosServico.ObterSomaDeGastoPorDia(requisicao)).ToIActionResult(this);
            }
            catch (Exception ex)
            {
                var problem = new DetalhesDeProblemas
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Titulo = "Erro interno no servidor",
                    Detalhe = ex.Message,
                    Instancia = HttpContext.Request.Path
                };

                return StatusCode(StatusCodes.Status500InternalServerError, problem);
            }
        }

        [HttpGet("GetFixedExpenses")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ObterGastosDiariosConsolidadosPagoVsNaoResposta>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(DetalhesDeProblemas))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(DetalhesDeProblemas))]
        public async Task<IActionResult> GetFixedExpenses([FromQuery] GetByMothDayRequest requisicao)
        {
            try
            {
                return (await controleDeGastosServico.ObterValorGastosFixosTotaisPagoVsNao(requisicao)).ToIActionResult(this);
            }
            catch (Exception ex)
            {
                var problem = new DetalhesDeProblemas
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Titulo = "Erro interno no servidor",
                    Detalhe = ex.Message,
                    Instancia = HttpContext.Request.Path
                };

                return StatusCode(StatusCodes.Status500InternalServerError, problem);
            }
        }

        [HttpGet("GetTotalDailyExpenses")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ObterTotalDeGastos>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(DetalhesDeProblemas))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(DetalhesDeProblemas))]
        public async Task<IActionResult> GetTotalDailyExpenses([FromQuery] GetByMothDayRequest requisicao)
        {
            try
            {
                return (await controleDeGastosServico.ObterTotalDeGastos(requisicao)).ToIActionResult(this);
            }
            catch (Exception ex)
            {
                var problem = new DetalhesDeProblemas
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Titulo = "Erro interno no servidor",
                    Detalhe = ex.Message,
                    Instancia = HttpContext.Request.Path
                };

                return StatusCode(StatusCodes.Status500InternalServerError, problem);
            }
        }
    }
}
