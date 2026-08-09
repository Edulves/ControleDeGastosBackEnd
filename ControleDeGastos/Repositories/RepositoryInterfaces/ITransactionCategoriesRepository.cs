using ExpensesControl.Models;

namespace ExpensesControl.Repositories.RepositoryInterfaces;

public interface ITransactionCategoriesRepository
{
    Task<List<TransactionCategory>> GetTransactionCategories();
    Task<TransactionCategory?> GetTransactionCategoryById(int id);
}
