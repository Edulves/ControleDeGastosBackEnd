using ExpensesControl.Data.PaginatedResult.PaginatedRequestDTO;
using Microsoft.EntityFrameworkCore;

namespace ExpensesControl.Data.PaginatedResult.Extentions;

public static class PaginatedRequestExtension
{
    public static async Task<(List<T> Items, int TotalCount)> PaginateAsync<T>(this IQueryable<T> query, int page, int itemsPerPage)
    {
        var totalCount = await query.CountAsync();

        var items = await query
                            .Skip((page - 1) * itemsPerPage)
                            .Take(itemsPerPage)
                            .ToListAsync();

        return (items, totalCount);
    }

    public static PagedResult<T> ToPagedResult<T>(this (List<T> Items, int TotalCount) source, int page, int itemsPerPage)
    {
        return new PagedResult<T>(items: source.Items,
                                  currentPage: page,
                                  itemsPerPage: itemsPerPage,
                                  totalItems: source.TotalCount);
    }

    /// <summary>
    /// Converte o tuple em PagedResult<T> usando um PagedRequest (avoida repetir página e qtde, passando apenas o DTO inteiro).
    /// </summary>
    public static PagedResult<T> ToPagedResult<T>(this (List<T> Items, int TotalCount) source, PaginatedRequest filter)
    {
        return source.ToPagedResult(page: filter.Page, itemsPerPage: filter.QTY);
    }
}



