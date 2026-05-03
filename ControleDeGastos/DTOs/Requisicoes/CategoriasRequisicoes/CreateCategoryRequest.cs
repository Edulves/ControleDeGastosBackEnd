using System.ComponentModel.DataAnnotations;

namespace ControleDeGastos.DTOs.Requisicoes.CategoriasRequisicoes
{
    public class CreateCategoryRequest
    {
        [Required]
        [MinLength(3)]
        public string NomeCategoria { get; set; } = string.Empty;
    }
}
