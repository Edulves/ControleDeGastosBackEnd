using ExpensesControl.Service.ServiceImplementations;
using ExpensesControl.Service.ServiceInterfaces;

namespace ExpensesControl.DependecyInjection;

public static class DependencyInjectionServices
{
    public static void AddServicesInjection(this IServiceCollection services)
    {
        services.AddScoped<IExpensesControlService, ExpensesControlService>();
    }
}
