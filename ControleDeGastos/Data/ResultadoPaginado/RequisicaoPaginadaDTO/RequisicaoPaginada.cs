using System.ComponentModel.DataAnnotations;

namespace ControleDeGastos.Data.ResultadoPaginado.RequisicaoPaginadaDTO;

/// <summary>
/// Base para todas as requests paginadas, com valores-padrão e validações.
/// </summary>
public abstract class RequisicaoPaginada
{
    /// <summary>
    /// Página atual _ Valor Padrão = 1.
    /// </summary>
    public int Pagina { get; set; } = 1;

    /// <summary>
    /// Quantidade de itens por página<br/> Valor Padrão = 10.
    /// </summary>
    [Range(0.01, int.MaxValue, ErrorMessage = "O valor deve ser maior que zero.")]
    public int QtdPorPagina { get; set; } = 10;
}

