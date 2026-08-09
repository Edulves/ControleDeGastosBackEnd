using ExpensesControl.Data.Contexto;
using ExpensesControl.Models;
using ExpensesControl.Queries;
using ExpensesControl.Repositories.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace ExpensesControl.Repositories.RepositoryImplementations;

public class TransactionCategoriesRepository(AppDbContext context) : ITransactionCategoriesRepository
{
    public IQueryable<TransactionCategory> GetTransactionCategoriesBase()
    {
        return context.TransactionCategories.FilterRemoveDeleted();
    }
    public async Task<List<TransactionCategory>> GetTransactionCategories()
    {
        return await GetTransactionCategoriesBase().OrderBy(x => x.Name).ToListAsync();
    }
    public async Task<TransactionCategory?> GetTransactionCategoryById(int id)
    {
        return await context.TransactionCategories.FindAsync(id);
    }
}
