FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# copy solution
#  COPY CatalogService.sln .

# copy projects
COPY ApiGateway.csproj ./

# restore
RUN dotnet restore ApiGateway.csproj

# copy everything
COPY . .

WORKDIR /src
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "ApiGateway.dll"]