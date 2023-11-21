using MetersSender;
using MetersSender.Api.Configuration;
using MetersSender.Api.SourceIntegration;
using MetersSender.Common;
using MetersSender.Common.Models;
using MetersSender.DataAccess.Database;
using MetersSender.DataAccess.Repository;
using MetersSender.Neodom;
using MetersSender.Saures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<ServiceSettings>(builder.Configuration.GetSection(InternalConsts.ServiceSettingsSectionName));

builder.Services.AddDbContext<MetersSenderDbContext>();

builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.Configure<MvcNewtonsoftJsonOptions>(o =>
{
    o.SerializerSettings.ContractResolver = new DefaultContractResolver
    {
        NamingStrategy = new CamelCaseNamingStrategy()
    };
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Macroservice API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "JSON Web Token to access resources. Example: Bearer {token}",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    var assemblyDirectoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

    if (!string.IsNullOrEmpty(assemblyDirectoryName))
    {// Set the comments path for the Swagger JSON and UI.
        foreach (var filePath in Directory.GetFiles(Path.Combine(assemblyDirectoryName), "*.xml"))
        {
            if (File.Exists(filePath))
            {
                c.IncludeXmlComments(filePath);
            }

        }
    }
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" } },
                        new [] { string.Empty }
                    }
                });

    c.CustomSchemaIds(x => x.FullName);
}).AddSwaggerGenNewtonsoftSupport();

builder.Services.AddScoped<IConfigurationApiService, ConfigurationApiService>();
builder.Services.AddScoped<ISourceIntegrationApiService, SourceIntegrationApiService>();
builder.Services.AddScoped<IRecepientIntegrationApiService, RecepientIntegrationApiService>();
builder.Services.AddScoped<IDatabaseRepository, PostgreSqlDatabaseRepository>();
builder.Services.AddScoped<ISourceIntegration, SauresSourceIntegration>();
builder.Services.AddScoped<IRecepientIntegration, NeodomRecepientIntegration>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

var serviceScopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
using (var serviceScope = serviceScopeFactory.CreateScope())
{
    var dbContext = serviceScope.ServiceProvider.GetService<MetersSenderDbContext>();
    dbContext.Database.EnsureCreated();
}

app.Run();
