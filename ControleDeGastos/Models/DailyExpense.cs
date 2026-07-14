using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpensesControl.Models
{
    public class DailyExpense
    {
        [Key]
        [Column("id_gastos_diarios")]
        public int DailyExpensesId { get; set; }
        [Column("data_do_lancamento")]
        public DateTime InputDate { get; set; }
        [Column("valor_gasto", TypeName = "decimal(18,2)")]
        public decimal ExpenseValue { get; set; }
        [Column("observacao")]
        public string? Note { get; set; }
        [Column("categoria_id")]
        public int? CategoryId { get; set; }
        [Column("deletado")]
        public string Deleted { get; set; } = string.Empty;

        // propriedades de navegacão
        [ForeignKey("CategoriaId")]
        public TransactionCategory? Category { get; set; }
    }
}
