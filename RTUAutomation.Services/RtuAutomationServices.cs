namespace RTUAutomation.Services;

public static class RtuAutomationServices
{
    public static void AddRtuAutomationServices(this IServiceCollection services)
    {
        services.AddScoped<DatabaseHelper>();

        #region PAS

        services.AddScoped<IPasRepository, PasRepository>();
        services.AddScoped<IPasService, PasService>();

        #endregion
    }
}