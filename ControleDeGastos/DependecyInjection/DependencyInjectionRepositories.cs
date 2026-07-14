using ExpensesControl.Repositories.InterfaceRepositories;
using ExpensesControl.Repositories.RepositoriesImplementation;
using ExpensesControl.Repositories.RepositoriesInterface;

namespace ExpensesControl.DependecyInjection;

public static class DependencyInjectionRepositories
{
    public static void AddDependencyInjectionRepositories(this IServiceCollection services)
    {
        services.AddScoped<IExpensesControlRepository, ExpensesControlRepository>();
        services.AddScoped<IGenericOperationsRepository, GenericOperationsRepository>();
    }
}
