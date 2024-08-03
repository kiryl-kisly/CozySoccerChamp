using CozySoccerChamp.Api.Extensions;
using CozySoccerChamp.Application.Extensions;
using CozySoccerChamp.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApiServices()
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseExceptionHandler();

app.MapControllers();

app.Run();