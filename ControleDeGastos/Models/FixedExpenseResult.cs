using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpensesControl.Models;

public class FixedExpenseResult
{
    [Key]
    [Column("idgastos_fixos")]
    public int FixedExpenseId { get; set; }
    [Column("descricao_gasto_fixo")]
    public string FixedExpenseDescription{ get; set; } = string.Empty;
    [Column("valor_gasto_fixo")]
    public decimal FixedExpenseValue { get; set; }
    [Column("pago")]
    public bool Paid { get; set; } = false;
    [Column("data_lancamento")]
    public DateTime InputDate { get;set; }
    [Column("deletado")]
    public string Deleted { get; set; } = string.Empty;
}
