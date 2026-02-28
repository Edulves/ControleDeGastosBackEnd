using ControleDeGastos.Data.PadraoDeResposta.Extensao;
using ControleDeGastos.Data.ResultadoPaginado;
using ControleDeGastos.DTOs.Erros;
using ControleDeGastos.DTOs.Requisicao.GastosDiarios;
using ControleDeGastos.DTOs.Resposta.GastosDiarios;
using ControleDeGastos.Servico.InterfaceServicos;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeGastos.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DespesasDiariasController(IControleDeGastosServico controleDeGastosServico) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResultadoPaginado<ObterGastosDiariosResposta>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(DetalhesDeProblemas))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(DetalhesDeProblemas))]
        public async Task<IActionResult> Get([FromQuery] ObterGastosDiariosRequisicao requisicao)
        {
            try
            {
                return (await controleDeGastosServico.ObterGastosDiarios(requisicao)).ToIActionResult(this);
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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(DetalhesDeProblemas))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(DetalhesDeProblemas))]
        public async Task<IActionResult> Post([FromBody] List<CriarLancamentoDeGastoDiarioRequisicao> requisicao)
        {
            try
            {
                return (await controleDeGastosServico.CriarLancamentosDeGastosDiarios(requisicao)).ToIActionResult(this);
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

        [HttpPut]

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(DetalhesDeProblemas))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(DetalhesDeProblemas))]
        public async Task<IActionResult> Put([FromBody] List<AtualizarGastosDiariosRequisicao> requisicao)
        {
            try
            {
                return (await controleDeGastosServico.AtualizarLancamentosDeGastosDiarios(requisicao)).ToIActionResult(this);
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

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(DetalhesDeProblemas))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(DetalhesDeProblemas))]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                return (await controleDeGastosServico.FalsoDeleteLancamentosDeGastosDiarios(id)).ToIActionResult(this);
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
