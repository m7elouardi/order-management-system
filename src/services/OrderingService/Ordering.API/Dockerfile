FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY OrderingService.sln .

COPY Ordering.API/Ordering.API.csproj Ordering.API/
COPY Ordering.Application/Ordering.Application.csproj Ordering.Application/
COPY Ordering.Domain/Ordering.Domain.csproj Ordering.Domain/
COPY Ordering.Infrastructure/Ordering.Infrastructure.csproj Ordering.Infrastructure/

RUN dotnet restore Ordering.API/Ordering.API.csproj

COPY . .

WORKDIR /src/Ordering.API
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Ordering.API.dll"]