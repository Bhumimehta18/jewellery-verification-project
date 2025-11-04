# Use the official .NET 9 SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy everything
COPY . ./

# Restore dependencies and publish the project
RUN dotnet restore
RUN dotnet publish -c Release -o out

# Use the runtime image for final build
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Copy published output from the build stage
COPY --from=build /app/out .

# Expose port 8080 for Render
EXPOSE 8080

# Run the application
ENTRYPOINT ["dotnet", "JewelleryVerificationProject.dll"]
