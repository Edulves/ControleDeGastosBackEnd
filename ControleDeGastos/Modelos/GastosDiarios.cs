using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleDeGastos.Models
{
    public class GastosDiarios
    {
        [Key]
        [Column("id_gastos")]
        public int IdGastos { get; set; }
        [Column("data_do_lancamento")]
        public DateTime DataDoLancamento { get; set; }
        [Column("valor_gasto", TypeName = "decimal(18,2)")]
        public decimal Valorgasto { get; set; }
        [Column("observacao")]
        public string? Observacao { get; set; }
        [Column("categoria_id")]
        public int? CategoriaId { get; set; }
        [Column("deletado")]
        public string Deletado { get; set; }

        // propriedades de navegacão
        [ForeignKey("CategoriaId")]
        public CategoriasDeLancamentos categoria { get; set; }
    }
}
