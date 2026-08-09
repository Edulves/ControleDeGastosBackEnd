using ExpensesControl.Models;

namespace ExpensesControl.Queries;

public static class FixedExpensesQueries
{
    public static IQueryable<FixedExpense> FilterRemoveDeleteds(this IQueryable<FixedExpense> query)
    {
        return query.Where(x => x.IsDeleted != true);
    }
    public static IQueryable<FixedExpense> FilterByPeriod(this IQueryable<FixedExpense> query, DateTime beginningOfPeriod, DateTime endOfPeriod)
    {
        if(beginningOfPeriod == DateTime.MinValue || endOfPeriod == DateTime.MinValue) 
            return query;

        if (beginningOfPeriod.Date > endOfPeriod.Date)
            return query;

        return query.Where(x => x.CreatedAt.Date >= beginningOfPeriod && x.CreatedAt.Date  <= endOfPeriod);
    }
    public static IQueryable<FixedExpense> FilterByMonthAndYear(this IQueryable<FixedExpense> query, int year, int month)
    {
        if (year == 0 || month == 0)
            return query;

        return query.Where(x => x.CreatedAt.Year == year && x.CreatedAt.Month == month);
    }
    public static IQueryable<FixedExpense> FilterByDescription(this IQueryable<FixedExpense> query, string description)
    {
        if (string.IsNullOrEmpty(description))
            return query;

        return query.Where(x => x.Description.Contains(description));
    }
}
