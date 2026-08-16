using ExpensesControl.Service.ServiceImplementations;
using ExpensesControl.Service.ServiceInterfaces;
using ExpensesControl.Services;

namespace ExpensesControl.DependecyInjection;

public static class DependencyInjectionServices
{
    public static void AddServicesInjection(this IServiceCollection services)
    {
        services.AddScoped<IConsolidationService, ConsolidationService>();
        services.AddScoped<IDailyExpensesService, DailyExpensesService>();
        services.AddScoped<IFixedExpensesService, FixedExpensesService>();
        services.AddScoped<ITransactionCategoriesService, TransactionCategoriesService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
    }
}
