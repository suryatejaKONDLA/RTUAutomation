namespace RTUAutomation.Api.Extensions;

public static class ServiceRegistrationExtension
{
    public static void AddAutoDiscoveredServices(this IServiceCollection services, params Assembly[] assemblies)
    {
        foreach (var assembly in assemblies)
        {
            var typesWithAttribute = assembly.GetTypes()
                .Where(type =>
                    type.IsClass &&
                    !type.IsAbstract &&
                    type.GetCustomAttribute<AutoRegisterServiceAttribute>() is not null)
                .ToList();

            foreach (var impl in typesWithAttribute)
            {
                var attribute = impl.GetCustomAttribute<AutoRegisterServiceAttribute>()!;
                var lifetime = attribute.Lifetime;

                var expectedInterface = impl.GetInterfaces().FirstOrDefault(i => $"I{impl.Name}" == i.Name);

                if (!impl.IsSealed &&
                    (impl.Namespace?.StartsWith("RTUAutomation.Repository") == true ||
                     impl.Namespace?.StartsWith("RTUAutomation.Services") == true))
                {
                    throw new InvalidOperationException($"Class '{impl.FullName}' in '{impl.Namespace}' must be sealed.");
                }


                RegisterService(services, expectedInterface ?? impl, impl, lifetime);
            }
        }
    }

    private static void RegisterService(IServiceCollection services, Type serviceType, Type implementationType, ServiceLifetime lifetime)
    {
        switch (lifetime)
        {
            case ServiceLifetime.Singleton:
                services.AddSingleton(serviceType, implementationType);
                break;
            case ServiceLifetime.Scoped:
                services.AddScoped(serviceType, implementationType);
                break;
            case ServiceLifetime.Transient:
                services.AddTransient(serviceType, implementationType);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(lifetime), lifetime, "Unsupported lifetime specified.");
        }
    }
}