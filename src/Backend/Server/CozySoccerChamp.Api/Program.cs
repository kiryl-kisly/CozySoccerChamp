using CozySoccerChamp.Api.Extensions;
using CozySoccerChamp.Application.Extensions;
using CozySoccerChamp.Infrastructure.Data;
using CozySoccerChamp.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApiServices()
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration);

builder.Services.AddCors();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseExceptionHandler();

app.UseCors(x => x.AllowAnyHeader()
    .AllowAnyMethod()
    .WithOrigins("http://localhost:3000", "http://87.228.25.125"));

app.MapControllers();

// TODO: подумать как это переделать в будущем
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
}

await app.RunAsync();