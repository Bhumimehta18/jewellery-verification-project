# -------------------------------
# STAGE 1: Build the application
# -------------------------------
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy everything
COPY . ./

# Restore dependencies
RUN dotnet restore

# Build and publish
RUN dotnet publish -c Release -o out

# -------------------------------
# STAGE 2: Run the application
# -------------------------------
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/out .

# Expose the port Render will use
EXPOSE 8080

# Run the app
CMD ["dotnet", "jewellery_certification.dll"]
