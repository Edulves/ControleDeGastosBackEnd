namespace ExpensesControl.Models;

public class TransactionCategory : Entity
{
    public int TransactionCategoryId { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<DailyExpense> DailyExpenses { get; set; } = [];
}
