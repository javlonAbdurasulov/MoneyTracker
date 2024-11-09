FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MoneyTracker/MoneyTracker.csproj", "MoneyTracker/"]
COPY ["MoneyTracker.Application/MoneyTracker.Application.csproj", "MoneyTracker.Application/"]
COPY ["MoneyTracker.Domain/MoneyTracker.Domain.csproj", "MoneyTracker.Domain/"]
COPY ["MoneyTracker.Infrastructure/MoneyTracker.Infrastructure.csproj", "MoneyTracker.Infrastructure/"]
RUN dotnet restore "./MoneyTracker/./MoneyTracker.csproj"
COPY . .
WORKDIR "/src/MoneyTracker"
RUN dotnet build "./MoneyTracker.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MoneyTracker.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MoneyTracker.dll"]