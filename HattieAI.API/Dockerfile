FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["HattieAI.API/HattieAI.API.csproj", "HattieAI.API/"]
COPY ["HattieAI.Domain/HattieAI.Domain.csproj", "HattieAI.Domain/"]
COPY ["HattieAI.Infrastructure/HattieAI.Infrastructure.csproj", "HattieAI.Infrastructure/"]
RUN dotnet restore "HattieAI.API/HattieAI.API.csproj"
COPY . .
WORKDIR "/src/HattieAI.API"
RUN dotnet build "HattieAI.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HattieAI.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HattieAI.API.dll"]
