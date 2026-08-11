using ExpensesControl.Models;

namespace ExpensesControl.Queries;

public static class FixedExpensesQueries
{

    public static IQueryable<FixedExpense> FilterByUserId(this IQueryable<FixedExpense> query, string id)
    {
        if (string.IsNullOrWhiteSpace(id))
            return query;

        return query.Where(x => x.UserId == id);
    }
    public static IQueryable<FixedExpense> FilterRemoveDeleteds(this IQueryable<FixedExpense> query)
    {
        return query.Where(x => x.IsDeleted != true);
    }
    public static IQueryable<FixedExpense> FilterByPeriod(this IQueryable<FixedExpense> query, DateOnly beginningOfPeriod, DateOnly endOfPeriod)
    {
        if(beginningOfPeriod == DateOnly.MinValue || endOfPeriod == DateOnly.MinValue) 
            return query;

        if (beginningOfPeriod > endOfPeriod)
            return query;

        return query.Where(x => x.FixedExpenseDate >= beginningOfPeriod && x.FixedExpenseDate  <= endOfPeriod);
    }
    public static IQueryable<FixedExpense> FilterByMonthAndYear(this IQueryable<FixedExpense> query, int year, int month)
    {
        if (year == 0 || month == 0)
            return query;

        return query.Where(x => x.FixedExpenseDate.Year == year && x.FixedExpenseDate.Month == month);
    }
    public static IQueryable<FixedExpense> FilterByDescription(this IQueryable<FixedExpense> query, string description)
    {
        if (string.IsNullOrEmpty(description))
            return query;

        return query.Where(x => x.Description.Contains(description));
    }
}
