using ApiProtection.Extensions;
using ApiProtection.StartupConfig;
using ApiProtection.Validation;
using AspNetCoreRateLimit;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddValidatorsFromAssemblyContaining<UserValidator>();
builder.Services.AddFluentValidationAutoValidationCustom();
builder.Services.AddResponseCaching();

builder.Services.AddMemoryCache();
builder.AddRateLimitServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseResponseCaching();
app.UseAuthorization();
app.MapControllers();
app.UseIpRateLimiting();

app.Run();