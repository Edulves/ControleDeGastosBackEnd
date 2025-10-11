using ClosedXML.Excel;
using ControleDeGastos.Data.Contexto;
using ControleDeGastos.Data.PadraoDeResposta.Extensao;
using ControleDeGastos.DTOs.Requisicao;
using ControleDeGastos.Models;
using ControleDeGastos.Servico.InterfaceServicos;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace ControleDeGastos.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ControleDeGastosController(AppDbContext context, IControleDeGastosServico controleDeGastosServico) : ControllerBase
    {
        [HttpGet("ObterGastos")]
        public async Task<IActionResult> ObterGastos([FromQuery] ObterGastosDiarios obterGastosDiarios)
        {
            return (await controleDeGastosServico.ObterGastosDiarios(obterGastosDiarios)).ToIActionResult(this);
        }

        [HttpGet("ObterCategorias")]
        public async Task<IActionResult> ObterCategorias()
        {
            return (await controleDeGastosServico.ObterCategoriasDeLancamentos()).ToIActionResult(this);
        }

        [HttpPost("SubirExcel")]
        public async Task<IActionResult> SubirExcel([FromForm] IFormFile planilha)
        {
            try
            {
                var dadosParaImportar = new List<GastosDiarios>();

                using (var stream = planilha.OpenReadStream())
                using (var workbook = new XLWorkbook(stream))
                {
                    var worksheet = workbook.Worksheet(1);
                    var rows = worksheet.RowsUsed().Skip(1);

                    foreach (var row in rows)
                    {
                        if (row.Cell(1).GetValue<string>() == "Total")
                            continue;

                        var dataDeLancamento = row.Cell(1).GetValue<DateTime>();
                        var valorGasto = row.Cell(2).GetValue<decimal>();
                        var observacao = row.Cell(3).GetValue<string>();
                        var idCategoria = row.Cell(5).GetValue<int>();

                        var novosLancamenos = new GastosDiarios(); 
                        novosLancamenos.DataDoLancamento = dataDeLancamento;
                        novosLancamenos.Valorgasto = valorGasto;
                        novosLancamenos.Observacao = observacao;
                        novosLancamenos.CategoriaId = idCategoria;

                        dadosParaImportar.Add(novosLancamenos);
                    }
                }

                await context.AddRangeAsync(dadosParaImportar);
                await context.SaveChangesAsync();

                return Ok(dadosParaImportar);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao processar o arquivo: {ex.Message}");
            }
        }
    }
}
