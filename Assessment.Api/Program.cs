using Assessment.Api.Middleware;
using Assessment.Api.Services;
using FluentValidation;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CarrierApiClient is pre-built — registered here as a singleton.
builder.Services.AddSingleton<ICarrierApiClient, CarrierApiClient>();

// TODO: Register your RatingService here with the appropriate lifetime
// Example:
//   builder.Services.AddScoped<IRatingService, RatingService>();

// Register FluentValidation validators from this assembly
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandler>();

app.MapEndpoints();

app.Run();

// Make Program accessible for testing
public partial class Program { }
