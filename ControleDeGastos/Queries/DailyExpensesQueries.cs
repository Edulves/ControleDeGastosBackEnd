using ExpensesControl.Models;

namespace ExpensesControl.Queries;

public static class DailyExpensesQueries
{
    public static IQueryable<DailyExpense> FilterByUserId(this IQueryable<DailyExpense> query, string id)
    {
        if (string.IsNullOrWhiteSpace(id))
            return query;

        return query.Where(x => x.UserId == id);
    }
    public static IQueryable<DailyExpense> FilterRemoveDeleted(this IQueryable<DailyExpense> query)
    { 
        return query.Where(x => x.IsDeleted != true);
    }
    public static IQueryable<DailyExpense> FilterByMonthAndYear(this IQueryable<DailyExpense> query, int year, int month)
    {
        if (year == 0 || month == 0)
            return query;
        
        return query.Where(x => x.ExpenseDate.Year == year && x.ExpenseDate.Month == month);
    }
    public static IQueryable<DailyExpense> FilterByTransactionPeriod(this IQueryable<DailyExpense> query, DateOnly beginningOfPeriod, DateOnly endOfPeriod)
    {
        if (beginningOfPeriod == DateOnly.MinValue || endOfPeriod == DateOnly.MinValue)
            return query;

        if(endOfPeriod < beginningOfPeriod)
            return query;

        return query.Where(x => x.ExpenseDate >= beginningOfPeriod && x.ExpenseDate <= endOfPeriod);
    }
    public static IQueryable<DailyExpense> FilterByCategory(this IQueryable<DailyExpense> query, string Category)
    {
        if(string.IsNullOrEmpty(Category))
            return query;
        
        return query.Where(x => x.TransactionCategory != null && x.TransactionCategory.Name.Contains(Category, StringComparison.CurrentCultureIgnoreCase));
    }
    public static IQueryable<DailyExpense> FilterByNote(this IQueryable<DailyExpense> query, string Observacao)
    {
        if (string.IsNullOrEmpty(Observacao))
            return query;

        return query.Where(x => x.Note != null && x.Note.Contains(Observacao, StringComparison.CurrentCultureIgnoreCase));
    }
}