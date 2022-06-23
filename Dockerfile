# NuGet restore
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src

COPY src/backend/DogsCompanion.App/* DogsCompanion.App/
COPY src/backend/DogsCompanion.Data/* DogsCompanion.Data/
RUN dotnet restore DogsCompanion.App/DogsCompanion.App.csproj
COPY . .

# publish
FROM build AS publish
WORKDIR /src/DogsCompanion.App
RUN dotnet publish -c Release -o /src/publish

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime
WORKDIR /app
COPY --from=publish /src/publish .

# heroku uses the following
ENV ASPNETCORE_ENVIRONMENT=Development
CMD ASPNETCORE_URLS=http://*:$PORT dotnet DogsCompanion.App.dll
