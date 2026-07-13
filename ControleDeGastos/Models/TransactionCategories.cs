using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpensesControl.Models;

public class TransactionCategories
{
    [Key]
    [Column("id_categoria_de_lancamentos")]
    public int TransactionCategoriesId { get; set; }
    [Column("nome_da_categoria")]
    public string CategoryName { get; set; } = string.Empty;
    [Column("deletado")]
    public string Deleted { get; set; } = string.Empty;
}
