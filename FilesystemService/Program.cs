using System.Security.Claims;
using FilesystemService.Authentication;
using FilesystemService.Database;

using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();

// Database
builder.Services.AddDbContext<ApplicationContext>(x => {
    var Hostname=Environment.GetEnvironmentVariable("DB_HOSTNAME") ?? "authdb";
    var Port=Environment.GetEnvironmentVariable("DB_PORT") ?? "5432";
    var Name=Environment.GetEnvironmentVariable("DB_NAME") ?? "postgres";
    var Username=Environment.GetEnvironmentVariable("DB_USERNAME") ?? "postgres";
    var Password=Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "postgres";
    x.UseNpgsql($"Server={Hostname}:{Port};Database={Name};Uid={Username};Pwd={Password};");
});

// Services

// Authorization
// builder.Services.AddAuthorizationBuilder()
//     .AddPolicy("Access", policy =>
//     {
//         policy.RequireClaim(ClaimTypes.AuthenticationMethod, "Access");
//     })
//     .AddPolicy("Microservice", policy =>
//     {
//         policy.RequireClaim(ClaimTypes.AuthenticationMethod, "Microservice");
//     });
// builder.Services.AddAuthentication("default")
// .AddScheme<AuthenticationSchemeOptions, JwtAuthenticationHandler>("default", options => 
// {
//     Console.WriteLine(options.ToString());
// });

    // .AddJwtBearer(options =>
    // {
    //     string issuer = Environment.GetEnvironmentVariable("AUTH_JWT_ISSUER") ?? "kvakvacloud";
    //     string audience = Environment.GetEnvironmentVariable("AUTH_JWT_AUDIENCE") ?? "kvakvacloud";
    //     string secret = Environment.GetEnvironmentVariable("AUTH_JWT_SECRET") ?? "";

    //     options.TokenValidationParameters = new TokenValidationParameters
    //     {
    //         ValidateIssuer = true,
    //         ValidIssuer = issuer,
    //         ValidateAudience = true,
    //         ValidAudience = audience,
    //         ValidateLifetime = true,
    //         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
    //         ValidateIssuerSigningKey = true
    //     };
    // });

builder.Services.AddControllers();

builder.Services.AddSwaggerGen(c =>
  {
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "KvaKvaCloud FilesystemService API",
        Contact = new OpenApiContact
        {
            Name = "Nikolai Papin",
            Email = "nikolai@weirdcat.ru"
        },
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Аутентификация при помощи токена типа Access, Refresh или Microservice.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        BearerFormat = "<type> <token>",
        Type = SecuritySchemeType.ApiKey
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });


    var xmlFile = Path.Combine(AppContext.BaseDirectory, "TestAPI.xml");
    if (File.Exists(xmlFile))
    {
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        c.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
    }
 });

var app = builder.Build();

app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(c =>
    {
        c.RouteTemplate = "/Filesystem/swagger/{documentName}/swagger.json";
    });
    app.UseSwaggerUI(c =>
    {
        c.RoutePrefix = "Filesystem";
        c.SwaggerEndpoint("/Filesystem/swagger/v1/swagger.json", "v1");
    });
}

app.Run();