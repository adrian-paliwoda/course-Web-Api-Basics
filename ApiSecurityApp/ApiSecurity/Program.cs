using ApiSecurity.Jwt;
using ApiSecurity.Policy;
using ApiSecurity.Swagger;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAuthorization(PolicyOption.Get);
builder.Services.AddAuthentication("Bearer").AddJwtBearer(options => JwtOption.GetBearerOptions(options, builder.Configuration));

builder.Services.AddSwaggerGen(SwaggerHelper.GetOptions);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();