using System.ComponentModel.DataAnnotations.Schema;

namespace ControleDeGastos.DTOs.Resposta.GastosDiarios
{
    public class ObterGastosDiariosResposta
    {
        public int IdGastosDiario { get; set; }
        public DateTime DataDoLancamento { get; set; }
        public decimal Valorgasto { get; set; }
        public string? Observacao { get; set; }
        public string NomeCategoria { get; set; }
    }
}
