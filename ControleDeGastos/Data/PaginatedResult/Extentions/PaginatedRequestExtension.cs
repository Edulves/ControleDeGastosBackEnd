using ExpensesControl.Data.PaginatedResult.PaginatedRequestDTO;
using Microsoft.EntityFrameworkCore;

namespace ExpensesControl.Data.PaginatedResult.Extentions;

public static class PaginatedRequestExtension
{
    public static async Task<(List<T> Items, int TotalCount)> PaginateAsync<T>(this IQueryable<T> query, int pagina, int itensPorPagina)
    {
        var totalCount = await query.CountAsync();

        var items = await query
                            .Skip((pagina - 1) * itensPorPagina)
                            .Take(itensPorPagina)
                            .ToListAsync();

        return (items, totalCount);
    }

    public static PagedResult<T> ToPagedResult<T>(this (List<T> Items, int TotalCount) source, int pagina, int itensPorPagina)
    {
        return new PagedResult<T>(items: source.Items,
                                  currentPage: pagina,
                                  itemsPerPage: itensPorPagina,
                                  totalItems: source.TotalCount);
    }

    /// <summary>
    /// Converte o tuple em PagedResult<T> usando um PagedRequest (avoida repetir página e qtde, passando apenas o DTO inteiro).
    /// </summary>
    public static PagedResult<T> ToPagedResult<T>(this (List<T> Items, int TotalCount) source, PaginatedRequest filtro)
    {
        return source.ToPagedResult(pagina: filtro.Page, itensPorPagina: filtro.QTY);
    }
}



