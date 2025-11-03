# Use official ASP.NET runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["jewellery-verification-project.csproj", "./"]
RUN dotnet restore "./jewellery-verification-project.csproj"
COPY . .
RUN dotnet publish "jewellery-verification-project.csproj" -c Release -o /app/publish

# Final stage
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "jewellery-verification-project.dll"]
