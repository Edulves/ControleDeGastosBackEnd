using System.ComponentModel.DataAnnotations;

namespace ControleDeGastos.DTOs.Requisicao.GastosDiarios
{
    public class AtualizarGastosDiariosRequisicao
    {   
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "IdGastos tem que ser maior que 0")]
        public int IdGastos { get; set; }
        [Required]
        public DateTime DataDoLancamento { get; set; }
        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "IdGastos tem que ser maior que 0")]
        public decimal Valorgasto { get; set; }
        [Required]
        public string? Observacao { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "CategoriaId tem que ser maior que 0")]
        public int CategoriaId { get; set; }
    }
}
