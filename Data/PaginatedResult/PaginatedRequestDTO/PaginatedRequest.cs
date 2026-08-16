using System.ComponentModel.DataAnnotations;

namespace ExpensesControl.Data.PaginatedResult.PaginatedRequestDTO;

/// <summary>
/// Base para todas as requests paginadas, com valores-padrão e validações.
/// </summary>
public abstract class PaginatedRequest
{
    /// <summary>
    /// Página atual _ Valor Padrão = 1.
    /// </summary>
    public int Page { get; set; } = 1;

    /// <summary>
    /// Quantidade de itens por página<br/> Valor Padrão = 10.
    /// </summary>
    [Range(1, int.MaxValue, ErrorMessage = "O valor deve ser maior que zero.")]
    public int QTY { get; set; } = 10;
}

