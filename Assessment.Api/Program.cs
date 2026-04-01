using Assessment.Api.Middleware;
using FluentValidation;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// TODO: Register your services here with appropriate lifetimes
// Example:
//   builder.Services.AddScoped<IRatingService, RatingService>();
//   builder.Services.AddSingleton<ICarrierApiClient, CarrierApiClient>();

// Register FluentValidation validators from this assembly
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

// TODO: Register AutoMapper profiles
//   builder.Services.AddAutoMapper(typeof(Program).Assembly);

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
