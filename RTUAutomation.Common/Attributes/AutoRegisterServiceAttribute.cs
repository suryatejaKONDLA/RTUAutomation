namespace RTUAutomation.Common.Attributes;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public sealed class AutoRegisterServiceAttribute(ServiceLifetime lifetime = ServiceLifetime.Scoped) : Attribute
{
    public ServiceLifetime Lifetime { get; } = lifetime;
}