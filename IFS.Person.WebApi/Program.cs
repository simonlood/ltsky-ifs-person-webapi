using System.Reflection;
using System.Text.Json.Serialization;
using FluentValidation.AspNetCore;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;
using IFS.Person.WebApi;
using IFS.Person.WebApi.Domain.Services;
using IFS.Person.WebApi.Domain.Services.Interfaces;
using IFS.Person.WebApi.Infrastructure.Filters;
using IFS.Person.WebApi.Infrastructure.Managers;
using IFS.Person.WebApi.Infrastructure.Managers.Interfaces;
using Serilog;
using IFS.Person.WebApi.Repository.PersonRepository.Interfaces;
using IFS.Person.WebApi.Repository.PersonRepository;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers(options => options.Filters.Add(typeof(ApiKeyAuthFilter))).AddFluentValidation(options =>
{
    options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    options.ImplicitlyValidateChildProperties = true;
}).AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
builder.Services.AddCors(options => { options.AddPolicy("AllowAllOrigin", corsPolicyBuilder => corsPolicyBuilder.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin()); });
builder.Services.AddOptions();
builder.Configuration.AddEnvironmentVariables("Settings");
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("Settings"));
builder.Services.AddHttpClient("IFSHttpClient", config => { config.BaseAddress = new Uri(builder.Configuration.GetValue<string>("Settings:IFSUri")); });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var scheme = new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.ApiKey,
        Description = "AWS ApiKey scheme (x-api-key: \"{token}\")",
        Name = "x-api-key",
        In = ParameterLocation.Header
    };

    c.AddSecurityDefinition("x-api-key", scheme);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "x-api-key"
                }
            },
            Array.Empty<string>()
        }
    });

    c.EnableAnnotations();
    c.SwaggerDoc("v1", new OpenApiInfo {Title = "IFS Person Api", Version = "v1", Description = " Internal version 0.0.1"});

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});
builder.Services.AddFluentValidationRulesToSwagger();

builder.Services.AddScoped<IHttpClientManager, HttpClientManager>();
builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();

builder.Services.AddAutoMapper(typeof(Program));

builder.Host.UseSerilog((context, services, configuration) => configuration
    .ReadFrom.Configuration(context.Configuration)
    .ReadFrom.Services(services)
    .Enrich.FromLogContext()); 

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSerilogRequestLogging();
app.UseCors("AllowAllOrigin");
app.UseSwagger();
app.UseSwaggerUI(c => { c.SwaggerEndpoint("v1/swagger.json", "IFS Person Api"); });

app.UseAuthorization();

app.MapControllers();

app.Run();
