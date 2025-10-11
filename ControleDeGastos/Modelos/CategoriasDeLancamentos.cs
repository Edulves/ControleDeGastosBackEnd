using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleDeGastos.Models
{
    public class CategoriasDeLancamentos
    {
        [Key]
        [Column("id_categoria_de_lancamentos")]
        public int IdCategoriaDeLancamentos { get; set; }
        [Column("nome_da_categoria")]
        public string NomeDaCategoria { get; set; } = string.Empty;
    }
}
