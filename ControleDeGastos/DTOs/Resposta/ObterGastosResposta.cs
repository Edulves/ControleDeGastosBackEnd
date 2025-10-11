using System.ComponentModel.DataAnnotations.Schema;

namespace ControleDeGastos.DTOs.Resposta
{
    public class ObterGastosResposta
    {
        public int IdGastos { get; set; }
        public DateTime DataDoLancamento { get; set; }
        public decimal Valorgasto { get; set; }
        public string? Observacao { get; set; }
        public string NomeCategoria { get; set; }
    }
}
