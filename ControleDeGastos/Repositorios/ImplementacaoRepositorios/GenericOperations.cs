using ExpensesControl.Data.Contexto;
using ExpensesControl.Repositories.RepositoriesInterface;

namespace ExpensesControl.Repositories.RepositoriesImplementation
{
    public class GenericOperations(AppDbContext context) : IGenericOperations
    {
        #region MetodosComuns
        public async Task<T> CriarAsync<T>(T entidade) where T : class
        {
            await context.AddAsync(entidade);
            await context.SaveChangesAsync();

            return entidade;
        }
        public async Task<List<T>> CriarAsync<T>(List<T> entidade) where T : class
        {
            await context.AddRangeAsync(entidade);
            await context.SaveChangesAsync();

            return entidade;
        }
        public async Task<T> AtualizarAsync<T>(T entidade) where T : class
        {
            context.Update(entidade);
            await context.SaveChangesAsync();

            return entidade;
        }
        public async Task<List<T>> AtualizarAsync<T>(List<T> entidade) where T : class
        {
            context.UpdateRange(entidade);
            await context.SaveChangesAsync();

            return entidade;
        }
        #endregion
    }
}
