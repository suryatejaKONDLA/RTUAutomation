var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddMudServices();
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddCitlAppServices();
builder.Services.AddStartupValidatorChecks(typeof(PhMasterModel).Assembly);

// Option 1: If API base address is from appsettings or environment:
var apiBaseUrl = builder.Configuration["ApiBaseUrl"] ?? builder.HostEnvironment.BaseAddress;

builder.Services.AddScoped(_ => new ApiContext
{
    ApiBaseUrl = apiBaseUrl,
});

// Register HttpClient (this can be default or named)
builder.Services.AddScoped(sp =>
{
    var context = sp.GetRequiredService<ApiContext>();
    return new HttpClient { BaseAddress = new(context.ApiBaseUrl) };
});

// Register NavigationManager automatically via framework DI

// Register BaseApiService
builder.Services.AddScoped<BaseApiService>();

await builder.Build().RunAsync();