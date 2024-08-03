global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Hosting;
global using Microsoft.Extensions.Logging;

global using CozySoccerChamp.Domain.Entities;
global using CozySoccerChamp.Domain.Entities.Common;
global using CozySoccerChamp.Domain.Entities.Soccer;
global using CozySoccerChamp.Domain.Enums;

global using CozySoccerChamp.Infrastructure.Data;
global using CozySoccerChamp.Infrastructure.Data.Configurations;
global using CozySoccerChamp.Infrastructure.BackgroundServices.TelegramWebhook;
global using CozySoccerChamp.Infrastructure.Repositories;
global using CozySoccerChamp.Infrastructure.Repositories.Abstractions;

global using Telegram.Bot;
global using Telegram.Bot.Types.Enums;