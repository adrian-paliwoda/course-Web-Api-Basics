using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using MonitoringApi.HealthChecks;
using WatchDog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks()
    .AddCheck<RandomHealthCheck>("Site HealthCheck")
    .AddCheck<RandomHealthCheck>("Database HealthCheck");
builder.Services.AddWatchDogServices();


builder.Services.AddHealthChecksUI(settings =>
    {
        settings.AddHealthCheckEndpoint("api", "/health");
        settings.SetEvaluationTimeInSeconds(5);
        settings.SetMinimumSecondsBetweenFailureNotifications(10);
    })
    .AddInMemoryStorage();


var app = builder.Build();
app.UseWatchDogExceptionLogger();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.MapHealthChecks("/health", new HealthCheckOptions()
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
app.MapHealthChecksUI();
app.UseWatchDog(model =>
{
    model.WatchPageUsername = app.Configuration.GetValue<string>("WatchDog:UserName");
    model.WatchPagePassword = app.Configuration.GetValue<string>("WatchDog:Password");
    model.Blacklist = "health"; 

});


app.Run();