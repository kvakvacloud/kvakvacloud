using System.Reflection;
using AuthService.Database;
using AuthService.Repository;
using AuthService.Services;
using AuthService.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables();

builder.Services.AddDbContext<ApplicationContext>(x => {
    var Hostname=Environment.GetEnvironmentVariable("DB_HOSTNAME") ?? "authdb";
    var Port=Environment.GetEnvironmentVariable("DB_PORT") ?? "5432";
    var Name=Environment.GetEnvironmentVariable("DB_NAME") ?? "postgres";
    var Username=Environment.GetEnvironmentVariable("DB_USERNAME") ?? "postgres";
    var Password=Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "postgres";
    x.UseNpgsql($"Server={Hostname}:{Port};Database={Name};Uid={Username};Pwd={Password};");
});

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRegistrationCodeRepository, RegistrationCodeRepository>();
builder.Services.AddScoped<IJwtUtils, JwtUtils>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IMicroserviceAuthService, MicroserviceAuthService>();

builder.Services.AddControllers();

builder.Services.AddSwaggerGen(c =>
  {
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "KvaKvaCloud AuthService API",
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
    c.RouteTemplate = "/Auth/swagger/{documentName}/swagger.json";
});
app.UseSwaggerUI(c =>
{
    c.RoutePrefix = "Auth";
    //c.SwaggerEndpoint("", "Sample API");
    c.SwaggerEndpoint("/Auth/swagger/v1/swagger.json", "v1");
});

app.Run();