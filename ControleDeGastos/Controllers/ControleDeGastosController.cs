using ControleDeGastos.Data.Contexto;
using ControleDeGastos.Data.PadraoDeResposta.Extensao;
using ControleDeGastos.Data.ResultadoPaginado;
using ControleDeGastos.DTOs.Erros;
using ControleDeGastos.DTOs.Requisicao;
using ControleDeGastos.DTOs.Resposta;
using ControleDeGastos.Models;
using ControleDeGastos.Servico.InterfaceServicos;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeGastos.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ControleDeGastosController(AppDbContext context, IControleDeGastosServico controleDeGastosServico) : ControllerBase
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
        #endregion


        //[HttpPost("SubirExcel")]
        //public async Task<IActionResult> SubirExcel([FromForm] IFormFile planilha)
        //{
        //    try
        //    {
        //        var dadosParaImportar = new List<GastosDiarios>();

        //        using (var stream = planilha.OpenReadStream())
        //        using (var workbook = new XLWorkbook(stream))
        //        {
        //            var worksheet = workbook.Worksheet(2);
        //            var rows = worksheet.RowsUsed().Skip(1);

        //            foreach (var row in rows)
        //            {
        //                if (row.Cell(1).GetValue<string>() == "Total")
        //                    continue;

        //                var dataDeLancamento = row.Cell(1).GetValue<DateTime>();
        //                var valorGasto = row.Cell(2).GetValue<decimal>();
        //                var observacao = row.Cell(3).GetValue<string>();
        //                var idCategoria = row.Cell(5).GetValue<int>();

        //                var novosLancamenos = new GastosDiarios(); 
        //                novosLancamenos.DataDoLancamento = dataDeLancamento;
        //                novosLancamenos.Valorgasto = valorGasto;
        //                novosLancamenos.Observacao = observacao;
        //                novosLancamenos.CategoriaId = idCategoria;

        //                dadosParaImportar.Add(novosLancamenos);
        //            }
        //        }

        //        await context.AddRangeAsync(dadosParaImportar);
        //        await context.SaveChangesAsync();

        //        return Ok(dadosParaImportar);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Erro ao processar o arquivo: {ex.Message}");
        //    }
        //}
    }
}
