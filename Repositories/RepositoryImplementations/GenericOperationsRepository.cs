using ExpensesControl.Data.Context;
using ExpensesControl.Repositories.RepositoriesInterface;

namespace ExpensesControl.Repositories.RepositoriesImplementation
{
    public class GenericOperationsRepository(AppDbContext context) : IGenericOperationsRepository
    {
        public async Task<T> CreateAsync<T>(T entity) where T : class
        {
            await context.AddAsync(entity);
            await context.SaveChangesAsync();

            return entity;
        }
        public async Task<List<T>> CreateAsync<T>(List<T> entity) where T : class
        {
            await context.AddRangeAsync(entity);
            await context.SaveChangesAsync();

            return entity;
        }
        public async Task<T> UpdateAsync<T>(T entity) where T : class
        {
            context.Update(entity);
            await context.SaveChangesAsync();

            return entity;
        }
        public async Task<List<T>> UpdateAsync<T>(List<T> entity) where T : class
        {
            context.UpdateRange(entity);
            await context.SaveChangesAsync();

            return entity;
        }
    }
}
