using ExpensesControl.Models;

namespace ExpensesControl.Queries;

public static class DailyExpensesQueries
{
    public static IQueryable<DailyExpenses> FilterRemoveDeleted(this IQueryable<DailyExpenses> query)
    { 
        return query.Where(x => x.Deleted != "*");
    }
    public static IQueryable<DailyExpenses> FilterByMonthAndYear(this IQueryable<DailyExpenses> query, int year, int month)
    {
        if (year == 0 || month == 0)
            return query;
        
        return query.Where(x => x.InputDate.Year == year && x.InputDate.Month == month);
    }
    public static IQueryable<DailyExpenses> FilterByTransactionPeriod(this IQueryable<DailyExpenses> query, DateTime beginningOfPeriod, DateTime endOfPeriod)
    {
        if (beginningOfPeriod == DateTime.MinValue || endOfPeriod == DateTime.MinValue)
            return query;

        if(endOfPeriod < beginningOfPeriod)
            return query;

        return query.Where(x => x.InputDate.Date >= beginningOfPeriod.Date && x.InputDate.Date <= endOfPeriod.Date);
    }

    public static IQueryable<DailyExpenses> FilterByCategory(this IQueryable<DailyExpenses> query, string Category)
    {
        if(string.IsNullOrEmpty(Category))
            return query;
        
        return query.Where(x => x.Category != null && x.Category.CategoryName.Contains(Category, StringComparison.CurrentCultureIgnoreCase));
    }

    public static IQueryable<DailyExpenses> FilterByNote(this IQueryable<DailyExpenses> query, string Observacao)
    {
        if (string.IsNullOrEmpty(Observacao))
            return query;

        return query.Where(x => x.Note != null && x.Note.Contains(Observacao, StringComparison.CurrentCultureIgnoreCase));
    }
}