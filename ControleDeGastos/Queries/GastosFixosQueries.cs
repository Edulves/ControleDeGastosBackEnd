using ControleDeGastos.Modelos;

namespace ControleDeGastos.Queries
{
    public static class GastosFixosQueries
    {
        public static IQueryable<FixedExpenseResult> FiltrarRemoverDeletados(this IQueryable<FixedExpenseResult> query)
        {
            return query.Where(x => x.Deletado != "*");
        }
        public static IQueryable<FixedExpenseResult> FiltrarPorPeriodo(this IQueryable<FixedExpenseResult> query, DateTime inicioPeriodo, DateTime fimPeriodo)
        {
            if(inicioPeriodo == DateTime.MinValue || fimPeriodo == DateTime.MinValue) 
                return query;

            if (inicioPeriodo.Date > fimPeriodo.Date)
                return query;

            return query.Where(x => x.DataDoLancamento.Date >= inicioPeriodo && x.DataDoLancamento.Date  <= fimPeriodo);
        }
        public static IQueryable<FixedExpenseResult> FiltrarPorMeseAno(this IQueryable<FixedExpenseResult> query, int ano, int mes)
        {
            if (ano == 0 || mes == 0)
                return query;

            return query.Where(x => x.DataDoLancamento.Year == ano && x.DataDoLancamento.Month == mes);
        }
        public static IQueryable<FixedExpenseResult> FiltrarPorDescricao(this IQueryable<FixedExpenseResult> query, string descricao)
        {
            if (string.IsNullOrEmpty(descricao))
                return query;

            return query.Where(x => x.DescricaoGastoFixo.Contains(descricao));
        }
    }
}
