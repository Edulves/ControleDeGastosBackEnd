using ControleDeGastos.Data.ResultadoPaginado.RequisicaoPaginadaDTO;
using Microsoft.EntityFrameworkCore;

namespace ControleDeGastos.Data.ResultadoPaginado.Extensoes;

public static class RequisicaoPaginadaExtensao
{
    public static async Task<(List<T> Items, int TotalCount)> PaginarAsync<T>(this IQueryable<T> query, int pagina, int itensPorPagina)
    {
        var totalCount = await query.CountAsync();

        var items = await query
            .Skip((pagina - 1) * itensPorPagina)
            .Take(itensPorPagina)
            .ToListAsync();

        return (items, totalCount);
    }

    public static ResultadoPaginado<T> ToPagedResult<T>(this (List<T> Items, int TotalCount) source, int pagina, int itensPorPagina)
    {
        return new ResultadoPaginado<T>(itens: source.Items,
                                  paginaAtual: pagina,
                                  itensPorPagina: itensPorPagina,
                                  totalItens: source.TotalCount);
    }

    /// <summary>
    /// Converte o tuple em PagedResult<T> usando um PagedRequest (avoida repetir página e qtde, passando apenas o DTO inteiro).
    /// </summary>
    public static ResultadoPaginado<T> ToPagedResult<T>(this (List<T> Items, int TotalCount) source, RequisicaoPaginada filtro)
    {
        return source.ToPagedResult(pagina: filtro.Pagina, itensPorPagina: filtro.QtdPorPagina);
    }
}



