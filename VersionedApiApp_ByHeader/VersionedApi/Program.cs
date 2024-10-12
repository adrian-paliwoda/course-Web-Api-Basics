using System.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var title = "Our versioned API";
    var description = "This is a Web API that demonstrates versioning";
    var term = new Uri("https://localhost:7243/terms");
    var license = new OpenApiLicense()
    {
        Name = "This is my full license information or a link to it."
    };
    var contact = new OpenApiContact()
    {
        Name =  "Helpdesk",
        Email = "help@mydomain.com",
        Url = new Uri("https://www.contact.com/")
    };
    
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = $"{title}",
        Description = description,
        TermsOfService = term,
        License = license,
        Contact = contact
    });
    
    options.SwaggerDoc("v2", new OpenApiInfo
    {
        Version = "v2",
        Title = $"{title}",
        Description = description,
        TermsOfService = term,
        License = license,
        Contact = contact
    });

});

builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(2, 0);
    options.ReportApiVersions = true;
    options.ApiVersionReader = new HeaderApiVersionReader("x-ms-version");
});

builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;    
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint($"/swagger/v2/swagger.json", "CurrentVersion");

    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();