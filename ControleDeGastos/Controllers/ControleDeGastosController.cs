using ControleDeGastos.Data.Contexto;
using ControleDeGastos.Data.PadraoDeResposta.Extensao;
using ControleDeGastos.Data.ResultadoPaginado;
using ControleDeGastos.DTOs.Erros;
using ControleDeGastos.DTOs.Requisicao.GastosDiarios;
using ControleDeGastos.DTOs.Requisicoes.CategoriasRequisicoes;
using ControleDeGastos.DTOs.Resposta.GastosDiarios;
using ControleDeGastos.Models;
using ControleDeGastos.Servico.InterfaceServicos;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeGastos.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ControleDeGastosController(IControleDeGastosServico controleDeGastosServico) : ControllerBase
    {
        #region GastosDiarios
        [HttpGet("ObterGastosDiarios")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResultadoPaginado<ObterGastosResposta>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(DetalhesDeProblemas))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(DetalhesDeProblemas))]
        public async Task<IActionResult> ObterGastos([FromQuery] ObterGastosDiariosRequisicao requisicao)
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

        [HttpPost("CriarLancamentosDeGastosDiario")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(DetalhesDeProblemas))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(DetalhesDeProblemas))]
        public async Task<IActionResult> CriarLancamentosDeGastosDiarios([FromBody] List<CriarLancamentoDeGastoDiarioRequisicao> requisicao)
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

        [HttpPut("AtualizarLancamentosDeGastosDiarios")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(DetalhesDeProblemas))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(DetalhesDeProblemas))]
        public async Task<IActionResult> AtualizarLancamentosDeGastosDiarios([FromBody] List<AtualizarGastosDiariosRequisicao> requisicao)
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

        [HttpDelete("FalsoDeleteLancamentosDeGastosDiarios")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(DetalhesDeProblemas))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(DetalhesDeProblemas))]
        public async Task<IActionResult> FalsoDeleteLancamentosDeGastosDiarios([FromQuery] int id)
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
        #endregion

        #region Categorias
        [HttpGet("ObterCategorias")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CategoriasDeLancamentos>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(DetalhesDeProblemas))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(DetalhesDeProblemas))]
        public async Task<IActionResult> ObterCategorias()
        {
            try
            {
                return (await controleDeGastosServico.ObterCategoriasDeLancamentos()).ToIActionResult(this);
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

        [HttpPost("CriarCategorias")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CategoriasDeLancamentos>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(DetalhesDeProblemas))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(DetalhesDeProblemas))]
        public async Task<IActionResult> CriarCategorias([FromBody] List<CriarCategoriaRequisicao> request)
        {
            try
            {
                return (await controleDeGastosServico.CriarCategorias(request)).ToIActionResult(this);
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

        [HttpPut("AtualizarCategorias")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CategoriasDeLancamentos>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(DetalhesDeProblemas))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(DetalhesDeProblemas))]
        public async Task<IActionResult> AtualizarCategorias([FromBody] List<CategoriasDeLancamentos> request)
        {
            try
            {
                return (await controleDeGastosServico.AtualizarCategorias(request)).ToIActionResult(this);
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

        [HttpDelete("FalsoDeleteCategoria")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CategoriasDeLancamentos>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(DetalhesDeProblemas))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(DetalhesDeProblemas))]
        public async Task<IActionResult> FalsoDeleteCategoria([FromQuery] int id)
        {
            try
            {
                return (await controleDeGastosServico.FalsoDeleteCategoria(id)).ToIActionResult(this);
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
        #endregion
    }
}
