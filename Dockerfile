# Use the SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the solution file and restore dependencies
COPY NZTechEvents.sln ./
COPY NZTechEvents.Web/NZTechEvents.Web.csproj NZTechEvents.Web/
COPY NZTechEvents.Core/NZTechEvents.Core.csproj NZTechEvents.Core/
COPY NZTechEvents.Infrastructure/NZTechEvents.Infrastructure.csproj NZTechEvents.Infrastructure/
RUN dotnet restore NZTechEvents.Web/NZTechEvents.Web.csproj

# Copy the remaining files and build the application
COPY NZTechEvents.Web/ NZTechEvents.Web/
COPY NZTechEvents.Core/ NZTechEvents.Core/
COPY NZTechEvents.Infrastructure/ NZTechEvents.Infrastructure/
WORKDIR /src/NZTechEvents.Web
RUN dotnet build -c Release -o /app/build

# Publish the application
RUN dotnet publish -c Release -o /app/publish

# Use the ASP.NET runtime image to run the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "NZTechEvents.Web.dll"]