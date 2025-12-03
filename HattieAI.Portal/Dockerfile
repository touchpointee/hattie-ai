FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["HattieAI.Portal/HattieAI.Portal.csproj", "HattieAI.Portal/"]
COPY ["HattieAI.Domain/HattieAI.Domain.csproj", "HattieAI.Domain/"]
COPY ["HattieAI.Infrastructure/HattieAI.Infrastructure.csproj", "HattieAI.Infrastructure/"]
RUN dotnet restore "HattieAI.Portal/HattieAI.Portal.csproj"
COPY . .
WORKDIR "/src/HattieAI.Portal"
RUN dotnet build "HattieAI.Portal.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HattieAI.Portal.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HattieAI.Portal.dll"]
