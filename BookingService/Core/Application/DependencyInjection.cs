using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Application;
public static class DependencyInjection
{
    public static void AddDependencyInjectionApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
    }
}
