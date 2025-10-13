using System.ComponentModel.DataAnnotations;

namespace ControleDeGastos.DTOs.Requisicao.GastosDiarios
{
    public class AtualizarGastosDiariosRequisicao
    {   
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "IdGastos tem que ser maior que 0")]
        public int IdGastosDiario { get; set; }
        public DateTime DataDoLancamento { get; set; } = DateTime.MinValue;
        public decimal Valorgasto { get; set; } = decimal.Zero;
        public string Observacao { get; set; } = string.Empty;
        public int CategoriaId { get; set; } = int.MinValue;
    }
}
