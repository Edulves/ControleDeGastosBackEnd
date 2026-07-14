using ExpensesControl.Models;

namespace ExpensesControl.Queries;

public static class TransactionsCategoriesQueries
{
    public static IQueryable<TransactionCategories> FilterRemoveDeleted(this IQueryable<TransactionCategories> query)
    {
        return query.Where(x => x.Deleted != "*");
    }
}
