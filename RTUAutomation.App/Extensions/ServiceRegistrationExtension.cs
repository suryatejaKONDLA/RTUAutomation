namespace RTUAutomation.App.Extensions;

public static class ServiceRegistrationExtension
{
    public static void AddCitlAppServices(this IServiceCollection services)
    {
        var assembly = typeof(BaseApiService).Assembly;

        var serviceTypes = assembly.GetTypes()
            .Where(type =>
                type.IsClass &&
                !type.IsAbstract &&
                type.GetCustomAttribute<AutoRegisterServiceAttribute>() is not null &&
                (
                    type.Namespace?.StartsWith("RTUAutomation.App.Services") == true
                ))
            .ToList();

        foreach (var implementationType in serviceTypes)
        {
            var attribute = implementationType.GetCustomAttribute<AutoRegisterServiceAttribute>()!;
            var lifetime = attribute.Lifetime;

            var expectedInterfaceName = $"I{implementationType.Name}";
            var interfaceType = implementationType.GetInterfaces().FirstOrDefault(i => i.Name == expectedInterfaceName);

            if (!implementationType.IsSealed)
            {
                throw new InvalidOperationException(
                    $"The class '{implementationType.FullName}' must be sealed to be auto-registered.");
            }

            RegisterService(services, interfaceType ?? implementationType, implementationType, lifetime);
        }

        services.AddScoped<BaseApiService>();
    }

    public static void AddStartupValidatorChecks(this IServiceCollection services, Assembly assembly)
    {
        var validatorTypes = assembly.GetTypes()
            .Where(type =>
                typeof(IValidator).IsAssignableFrom(type) &&
                type.IsClass &&
                !type.IsAbstract &&
                type.BaseType?.IsGenericType == true &&
                type.BaseType.GetGenericTypeDefinition() == typeof(AbstractValidator<>))
            .ToList();

        foreach (var validator in validatorTypes.Where(validator => !validator.IsPublic))
        {
            throw new InvalidOperationException($"Validator class '{validator.FullName}' must be declared as public.");
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
                throw new ArgumentOutOfRangeException(nameof(lifetime), lifetime, null);
        }
    }
}