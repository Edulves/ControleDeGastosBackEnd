using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleDeGastos.DTOs.Requisicao.GastosDiarios
{
    public class CriarLancamentoDeGastoDiarioRequisicao
    {
        [Required]
        public DateTime DataDoLancamento { get; set; }
        [Required]
        public decimal Valorgasto { get; set; }
        public string? Observacao { get; set; }
        [Required]
        public int CategoriaId { get; set; }
    }
}
