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
        public static IQueryable<GastosFixos> FiltrarPorMeseAno(this IQueryable<GastosFixos> query, int ano, int mes)
        {
            if (ano == 0 || mes == 0)
                return query;

            return query.Where(x => x.DataDoLancamento.Year == ano && x.DataDoLancamento.Month == mes);
        }
        public static IQueryable<GastosFixos> FiltrarPorDescricao(this IQueryable<GastosFixos> query, string descricao)
        {
            if (string.IsNullOrEmpty(descricao))
                return query;

            return query.Where(x => x.DescricaoGastoFixo.Contains(descricao));
        }
    }
}
