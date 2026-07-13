using ExpensesControl.Models;

namespace ExpensesControl.Queries
{
    public static class CategoriasDeLancamentosQueries
    {
        public static IQueryable<TransactionCategories> FiltrarRemoverDeletados(this IQueryable<TransactionCategories> query)
        {
            return query.Where(x => x.Deleted != "*");
        }
    }
}
