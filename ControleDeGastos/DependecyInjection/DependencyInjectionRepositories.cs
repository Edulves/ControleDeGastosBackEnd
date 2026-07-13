using ExpensesControl.Repositories.RepositoriesImplementation;
using ExpensesControl.Repositories.RepositoriesInterface;

namespace ExpensesControl.DependecyInjection;

public static class DependencyInjectionRepositories
{
    public static void AddDependencyInjectionRepositories(this IServiceCollection services)
    {
        services.AddScoped<IExpensesControlRepositories, ExpensesControlRepositories>();
        services.AddScoped<IGenericOperations, GenericOperations>();
    }
}
