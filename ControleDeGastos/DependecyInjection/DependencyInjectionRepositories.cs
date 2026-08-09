using ExpensesControl.Repositories.RepositoriesImplementation;
using ExpensesControl.Repositories.RepositoriesInterface;
using ExpensesControl.Repositories.RepositoryImplementations;
using ExpensesControl.Repositories.RepositoryInterfaces;

namespace ExpensesControl.DependecyInjection;

public static class DependencyInjectionRepositories
{
    public static void AddDependencyInjectionRepositories(this IServiceCollection services)
    {
        services.AddScoped<IDailyExpensesRepository, DailyExpensesRepository>();
        services.AddScoped<IFixedExpensesRepository, FixedExpensesRepository>();
        services.AddScoped<IGenericOperationsRepository, GenericOperationsRepository>();
        services.AddScoped<ITransactionCategoriesRepository, TransactionCategoriesRepository>();
    }
}
