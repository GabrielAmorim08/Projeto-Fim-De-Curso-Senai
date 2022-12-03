using back.Services;
using Trevisharp.Security.Jwt;

int tokenSize = 32;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<TokenService>(
    provinder => new TokenService(tokenSize));

builder.Services.AddSingleton<CryptoService>(
    provinder => new CryptoService()
    {
        InternalKeySize = 16,
        UpdatePeriod = TimeSpan.FromMinutes(18)
    });
    
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

app.Run();
