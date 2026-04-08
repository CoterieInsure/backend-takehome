using Assessment.Data;
using Assessment.Api.Features.BusinessFactors;
using Assessment.Api.Features.BusinessFactors.Services;
using Assessment.Api.Features.CarrierEligibility;
using Assessment.Api.Features.CarrierEligibility.Services;
using Assessment.Api.Features.StateFactors;
using Assessment.Api.Features.StateFactors.Services;
using FluentValidation;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Assessment API",
        Version = "v1"
    });
});

// DB connection factory
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;
builder.Services.AddSingleton<IDbConnectionFactory>(new SqliteConnectionFactory(connectionString));

// Services
builder.Services.AddScoped<IBusinessFactorService, BusinessFactorService>();
builder.Services.AddScoped<IStateFactorService, StateFactorService>();
builder.Services.AddScoped<ICarrierEligibilityService, CarrierEligibilityService>();

var app = builder.Build();

// Database initialization
DatabaseInitializer.InitializeDatabase(connectionString);

// Swagger
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Assessment API v1");
    options.RoutePrefix = "swagger";
});

app.MapControllers();

app.Run();
