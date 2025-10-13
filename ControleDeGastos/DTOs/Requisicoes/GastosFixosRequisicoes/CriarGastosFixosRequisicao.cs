using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleDeGastos.DTOs.Requisicoes.GastosFixosRequisicoes
{
    public class CriarGastosFixosRequisicao
    {
        [Required]
        [Length(3, 100, ErrorMessage = "Descrição do gastoFixo deve ter entre 3 e 100 caracteres")]
        public string DescricaoGastoFixo { get; set; }
        [Required]
        public decimal ValorGastoFixo { get; set; }
        [Required]
        public DateTime DataLancamento { get; set; }
    }
}
