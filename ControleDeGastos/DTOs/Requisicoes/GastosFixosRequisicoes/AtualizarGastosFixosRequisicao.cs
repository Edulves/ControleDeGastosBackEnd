using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleDeGastos.DTOs.Requisicoes.GastosFixosRequisicoes
{
    public class AtualizarGastosFixosRequisicao
    {
        [Required]
        public int IdGastosFixos { get; set; }
        public string DescricaoGastoFixo { get; set; } = string.Empty;
        public decimal ValorGastoFixo { get; set; } = 0;
        public bool? Pago { get; set; } = null;
        public DateTime DataDoLancamento { get; set; } = DateTime.MinValue;
    }
}
