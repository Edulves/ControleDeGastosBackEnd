using ControleDeGastos.Modelos;

namespace ControleDeGastos.Queries
{
    public static class GastosFixosQueries
    {
        public static IQueryable<GastosFixos> FiltrarRemoverDeletados(this IQueryable<GastosFixos> query)
        {
            return query.Where(x => x.Deletado != "*");
        }
        public static IQueryable<GastosFixos> FiltrarPorPeriodo(this IQueryable<GastosFixos> query, DateTime inicioPeriodo, DateTime fimPeriodo)
        {
            if(inicioPeriodo == DateTime.MinValue || fimPeriodo == DateTime.MinValue) 
                return query;

            if (inicioPeriodo.Date > fimPeriodo.Date)
                return query;

            return query.Where(x => x.DataDoLancamento.Date >= inicioPeriodo && x.DataDoLancamento.Date  <= fimPeriodo);
        }

        public static IQueryable<GastosFixos> FiltrarPorDescricao(this IQueryable<GastosFixos> query, string descricao)
        {
            if (string.IsNullOrEmpty(descricao))
                return query;

            return query.Where(x => x.DescricaoGastoFixo.Contains(descricao));
        }
    }
}
