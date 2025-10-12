using ControleDeGastos.Models;

namespace ControleDeGastos.Queries
{
    public static class GastosDiariosQuery
    {
        public static IQueryable<GastosDiarios> FiltraRemoverDeletados(this IQueryable<GastosDiarios> query)
        { 
            return query.Where(x => x.Deletado != "*");
        }
    }
}