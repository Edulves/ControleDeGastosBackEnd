using ExpensesControl.Models;

namespace ExpensesControl.Queries
{
    public static class GastosDiariosQueries
    {
        public static IQueryable<DailyExpenses> FiltrarRemoverDeletados(this IQueryable<DailyExpenses> query)
        { 
            return query.Where(x => x.Deleted != "*");
        }
        public static IQueryable<DailyExpenses> FiltrarPorMeseAno(this IQueryable<DailyExpenses> query, int ano, int mes)
        {
            if (ano == 0 || mes == 0)
                return query;
            
            return query.Where(x => x.InputDate.Year == ano && x.InputDate.Month == mes);
        }
        public static IQueryable<DailyExpenses> FiltrarPorPeriodoDeLancamento(this IQueryable<DailyExpenses> query, DateTime dataIncio, DateTime dataFim)
        {
            if (dataIncio == DateTime.MinValue || dataFim == DateTime.MinValue)
                return query;

            if(dataFim < dataIncio)
                return query;

            return query.Where(x => x.InputDate.Date >= dataIncio.Date && x.InputDate.Date <= dataFim.Date);
        }

        public static IQueryable<DailyExpenses> FiltrarPorCategorias(this IQueryable<DailyExpenses> query, string Categoria)
        {
            if(string.IsNullOrEmpty(Categoria))
                return query;
            
            return query.Where(x => x.Category.CategoryName.Contains(Categoria.ToLower()));
        }

        public static IQueryable<DailyExpenses> FiltrarPorObservacao(this IQueryable<DailyExpenses> query, string Observacao)
        {
            if (string.IsNullOrEmpty(Observacao))
                return query;

            return query.Where(x => x.Note.Contains(Observacao.ToLower()));
        }
    }
}