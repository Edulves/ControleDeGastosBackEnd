using ExpensesControl.Models;

namespace ExpensesControl.Queries;

public static class TransactionsCategoriesQueries
{
    public static IQueryable<TransactionCategory> FilterRemoveDeleted(this IQueryable<TransactionCategory> query)
    {
        return query.Where(x => x.Deleted != "*");
    }
}
