using ControleDeGastos.Modelos;

namespace ControleDeGastos.Queries
{
    public static class GastosDiariosQueries
    {
        public static IQueryable<GastosDiarios> FiltrarRemoverDeletados(this IQueryable<GastosDiarios> query)
        { 
            return query.Where(x => x.Deletado != "*");
        }
        public static IQueryable<GastosDiarios> FiltrarPorMeseAno(this IQueryable<GastosDiarios> query, int ano, int mes)
        {
            if (ano == 0 || mes == 0)
                return query;
            
            return query.Where(x => x.DataDoLancamento.Year == ano && x.DataDoLancamento.Month == mes);
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

        public static IQueryable<GastosDiarios> FiltrarPorObservacao(this IQueryable<GastosDiarios> query, string Observacao)
        {
            if (string.IsNullOrEmpty(Observacao))
                return query;

            return query.Where(x => x.Observacao.Contains(Observacao.ToLower()));
        }
    }
}