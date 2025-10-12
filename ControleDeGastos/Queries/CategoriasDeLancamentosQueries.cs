using ControleDeGastos.Models;

namespace ControleDeGastos.Queries
{
    public static class CategoriasDeLancamentosQueries
    {
        public static IQueryable<CategoriasDeLancamentos> FiltrarRemoverDeletados(this IQueryable<CategoriasDeLancamentos> query)
        {
            return query.Where(x => x.Deletado != "*");
        }
    }
}
