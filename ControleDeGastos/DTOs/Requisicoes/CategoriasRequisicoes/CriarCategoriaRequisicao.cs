using System.ComponentModel.DataAnnotations;

namespace ControleDeGastos.DTOs.Requisicoes.CategoriasRequisicoes
{
    public class CriarCategoriaRequisicao
    {
        [Required]
        [MinLength(3)]
        public string NomeCategoria { get; set; } = string.Empty;
    }
}
