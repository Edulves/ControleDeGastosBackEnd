using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleDeGastos.Modelos
{
    public class GastosFixos
    {
        [Key]
        [Column("idgastos_fixos")]
        public int IdGastosFixos { get; set; }
        [Column("descricao_gasto_fixo")]
        public string DescricaoGastoFixo{ get; set; }
        [Column("valor_gasto_fixo")]
        public decimal ValorGastoFixo { get; set; }
        [Column("pago")]
        public bool Pago { get; set; } = false;
        [Column("data_lancamento")]
        public DateTime DataDoLancamento { get;set; }
        [Column("deletado")]
        public string Deletado { get; set; } = string.Empty;
    }
}
