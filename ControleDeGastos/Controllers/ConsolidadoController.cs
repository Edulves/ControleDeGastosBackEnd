using ControleDeGastos.Data.PadraoDeResposta.Extensao;
using ControleDeGastos.DTOs.Erros;
using ControleDeGastos.DTOs.Requisicoes.ConsolidadoRequisicoes;
using ControleDeGastos.Servico.InterfaceServicos;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeGastos.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ConsolidadoController(IControleDeGastosServico controleDeGastosServico) : ControllerBase
    {
        [HttpGet("ObterSomaDeGastoPorCategoria")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ObterGastosDiariosConsolidadosPorCategoriaRequisicao>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(DetalhesDeProblemas))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(DetalhesDeProblemas))]
        public async Task<IActionResult> GetExpensesPerCategory([FromQuery] ObterGastosDiariosConsolidadosPorCategoriaRequisicao requisicao)
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

        [HttpGet("ObterSomaDeGastoPorDia")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ObterGastosDiariosConsolidadosPorCategoriaRequisicao>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(DetalhesDeProblemas))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(DetalhesDeProblemas))]
        public async Task<IActionResult> GetExpensesPerDay([FromQuery] ObterGastosDiariosConsolidadosPorMesAnoRequisicao requisicao)
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

        [HttpGet("ObterValorTotaisGastosFixos")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ObterGastosDiariosConsolidadosPorCategoriaRequisicao>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(DetalhesDeProblemas))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(DetalhesDeProblemas))]
        public async Task<IActionResult> GetFixedExpenses([FromQuery] ObterGastosDiariosConsolidadosPorMesAnoRequisicao requisicao)
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

        [HttpGet("ObterTotalDeGastosDiarios")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ObterGastosDiariosConsolidadosPorCategoriaRequisicao>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(DetalhesDeProblemas))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(DetalhesDeProblemas))]
        public async Task<IActionResult> GetTotalDailyExpenses([FromQuery] ObterGastosDiariosConsolidadosPorMesAnoRequisicao requisicao)
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
