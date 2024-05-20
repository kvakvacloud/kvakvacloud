using ConfigurationService.Database;
using ConfigurationService.Repository;
using ConfigurationService.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables();

builder.Services.AddDbContext<ApplicationContext>(x => {
    var Hostname=Environment.GetEnvironmentVariable("DB_HOSTNAME") ?? "database";
    var Port=Environment.GetEnvironmentVariable("DB_PORT") ?? "5432";
    var Name=Environment.GetEnvironmentVariable("DB_NAME") ?? "postgres";
    var Username=Environment.GetEnvironmentVariable("DB_USERNAME") ?? "postgres";
    var Password=Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "postgres";
    x.UseNpgsql($"Server={Hostname}:{Port};Database={Name};Uid={Username};Pwd={Password};");
});

builder.Services.AddScoped<IGlobalSettingRepository, GlobalSettingRepository>();
builder.Services.AddScoped<IGlobalSettingsService, GlobalSettingsService>();

builder.Services.AddControllers();

builder.Services.AddSwaggerGen(c =>
  {
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "KvaKvaCloud ConfigurationService API",
        Contact = new OpenApiContact
        {
            Name = "Nikolai Papin",
            Email = "nikolai@weirdcat.ru"
        },
    });

    var xmlFile = Path.Combine(AppContext.BaseDirectory, "TestAPI.xml");
    if (File.Exists(xmlFile))
    {
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        c.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
    }
 });

var app = builder.Build();

app.UseRouting();
app.MapControllers();

app.UseSwagger(c =>
{
    c.RouteTemplate = "/Configuration/swagger/{documentName}/swagger.json";
});
app.UseSwaggerUI(c =>
{
    c.RoutePrefix = "Auth";
    c.SwaggerEndpoint("/Configuration/swagger/v1/swagger.json", "v1");
});

app.Run();