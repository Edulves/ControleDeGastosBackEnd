using ControleDeGastos.Models;

namespace ControleDeGastos.Queries
{
    public static class GastosDiariosQueries
    {
        public static IQueryable<GastosDiarios> FiltrarRemoverDeletados(this IQueryable<GastosDiarios> query)
        { 
            return query.Where(x => x.Deletado != "*");
        }
    }
}