const string appName = "RTUAutomation.API";
const string apiTitle = "RTU Automation API";
const string apiVersion = "v1";
const string openApiPath = $"/{apiVersion}/api.json";
const string swaggerUiPath = "/swagger";
const string generalLogFile = "Logs/Log-.txt";
const string authLogFile = "Logs/Authentication-Log-.txt";

Directory.CreateDirectory("Logs");

const string logTemplate = "[{Timestamp:yyyy-MM-dd HH:mm:ss}] [{Level:u3}] {Message}{NewLine}{Exception}";

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .MinimumLevel.Override("System", LogEventLevel.Warning)
    .Enrich.FromLogContext()
    .Enrich.WithProperty("Application", appName)
    .WriteTo.Console(outputTemplate: logTemplate)
    .WriteTo.File(generalLogFile, LogEventLevel.Information, rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 30, outputTemplate: logTemplate)
    .WriteTo.Logger(lc => lc
        .Filter.ByIncludingOnly(le =>
            le.Properties.ContainsKey("SourceContext") &&
            le.Properties["SourceContext"].ToString().Contains("RTUAutomation.Services.AuthenticationServices"))
        .WriteTo.File(authLogFile, LogEventLevel.Information, rollingInterval: RollingInterval.Day,
            retainedFileCountLimit: 30, outputTemplate: logTemplate))
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument(config =>
{
    config.Title = apiTitle;
    config.Version = apiVersion;
});

builder.Services.AddHealthChecks();
builder.Services.AddControllers();
builder.Services.AddRtuAutomationServices();

var app = builder.Build();

app.UseSerilogRequestLogging();
app.UseHttpsRedirection();

app.UseOpenApi(opt => opt.Path = openApiPath);
app.UseSwaggerUi(settings =>
{
    settings.Path = swaggerUiPath;
    settings.DocumentTitle = apiTitle;
    settings.PersistAuthorization = true;
    settings.DocExpansion = "none";
    settings.SwaggerRoutes.Clear();
    settings.SwaggerRoutes.Add(new("RTU API", openApiPath));
});

app.MapOpenApi();
app.MapControllers();
await app.RunAsync();
