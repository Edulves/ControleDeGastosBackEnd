using ControleDeGastos.Models;

namespace ControleDeGastos.Queries
{
    public static class GastosDiariosQueries
    {
        public static IQueryable<GastosDiarios> FiltrarRemoverDeletados(this IQueryable<GastosDiarios> query)
        { 
            return query.Where(x => x.Deletado != "*");
        }
        public static IQueryable<GastosDiarios> FiltrarPorPeriodoDeLancamento(this IQueryable<GastosDiarios> query, DateTime dataIncio, DateTime dataFim)
        {
            if (dataIncio == DateTime.MinValue || dataFim == DateTime.MinValue)
                return query;

            if(dataFim < dataIncio)
                return query;

            return query.Where(x => x.DataDoLancamento.Date >= dataIncio.Date && x.DataDoLancamento.Date <= dataFim.Date);
        }

        public static IQueryable<GastosDiarios> FiltrarPorCategorias(this IQueryable<GastosDiarios> query, string Categoria)
        {
            if(string.IsNullOrEmpty(Categoria))
                return query;
            
            return query.Where(x => x.categoria.NomeDaCategoria.Contains(Categoria.ToLower()));
        }
    }
}