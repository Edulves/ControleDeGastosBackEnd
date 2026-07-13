using ExpensesControl.Modelos;

namespace ExpensesControl.Queries
{
    public static class CategoriasDeLancamentosQueries
    {
        public static IQueryable<EntryCategories> FiltrarRemoverDeletados(this IQueryable<EntryCategories> query)
        {
            return query.Where(x => x.Deletado != "*");
        }
    }
}
