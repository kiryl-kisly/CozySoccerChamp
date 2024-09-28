FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8000
EXPOSE 8001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["Server/CozySoccerChamp.Api/CozySoccerChamp.Api.csproj", "Server/CozySoccerChamp.Api/"]
COPY ["Server/CozySoccerChamp.Application/CozySoccerChamp.Application.csproj", "Server/CozySoccerChamp.Application/"]
COPY ["Server/CozySoccerChamp.Infrastructure/CozySoccerChamp.Infrastructure.csproj", "Server/CozySoccerChamp.Infrastructure/"]
COPY ["Server/CozySoccerChamp.Domain/CozySoccerChamp.Domain.csproj", "Server/CozySoccerChamp.Domain/"]
COPY ["ExternalServices/CozySoccerChamp.External.SoccerApi/CozySoccerChamp.External.SoccerApi.csproj", "ExternalServices/CozySoccerChamp.External.SoccerApi/"]

RUN dotnet restore "Server/CozySoccerChamp.Api/CozySoccerChamp.Api.csproj"

COPY . .

WORKDIR "/src/Server/CozySoccerChamp.Api"
RUN dotnet build "CozySoccerChamp.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CozySoccerChamp.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CozySoccerChamp.Api.dll"]
