FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# copy solution
COPY CatalogService.sln .

# copy projects
COPY Catalog.API/Catalog.API.csproj Catalog.API/
COPY Catalog.Application/Catalog.Application.csproj Catalog.Application/
COPY Catalog.Domain/Catalog.Domain.csproj Catalog.Domain/
COPY Catalog.Infrastructure/Catalog.Infrastructure.csproj Catalog.Infrastructure/

# restore
RUN dotnet restore Catalog.API/Catalog.API.csproj

# copy everything
COPY . .

WORKDIR /src/Catalog.API
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Catalog.API.dll"]