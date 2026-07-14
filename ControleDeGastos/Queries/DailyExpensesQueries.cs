using ExpensesControl.Models;

namespace ExpensesControl.Queries;

public static class DailyExpensesQueries
{
    public static IQueryable<DailyExpense> FilterRemoveDeleted(this IQueryable<DailyExpense> query)
    { 
        return query.Where(x => x.Deleted != "*");
    }
    public static IQueryable<DailyExpense> FilterByMonthAndYear(this IQueryable<DailyExpense> query, int year, int month)
    {
        if (year == 0 || month == 0)
            return query;
        
        return query.Where(x => x.InputDate.Year == year && x.InputDate.Month == month);
    }
    public static IQueryable<DailyExpense> FilterByTransactionPeriod(this IQueryable<DailyExpense> query, DateTime beginningOfPeriod, DateTime endOfPeriod)
    {
        if (beginningOfPeriod == DateTime.MinValue || endOfPeriod == DateTime.MinValue)
            return query;

        if(endOfPeriod < beginningOfPeriod)
            return query;

        return query.Where(x => x.InputDate.Date >= beginningOfPeriod.Date && x.InputDate.Date <= endOfPeriod.Date);
    }

    public static IQueryable<DailyExpense> FilterByCategory(this IQueryable<DailyExpense> query, string Category)
    {
        if(string.IsNullOrEmpty(Category))
            return query;
        
        return query.Where(x => x.Category != null && x.Category.CategoryName.Contains(Category, StringComparison.CurrentCultureIgnoreCase));
    }

    public static IQueryable<DailyExpense> FilterByNote(this IQueryable<DailyExpense> query, string Observacao)
    {
        if (string.IsNullOrEmpty(Observacao))
            return query;

        return query.Where(x => x.Note != null && x.Note.Contains(Observacao, StringComparison.CurrentCultureIgnoreCase));
    }
}