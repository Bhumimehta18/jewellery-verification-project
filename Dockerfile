# Use the official .NET SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the csproj and restore as distinct layers
COPY ["jewellery vertification project.csproj", "./"]
RUN dotnet restore "jewellery vertification project.csproj"

# Copy everything else and build the project
COPY . .
RUN dotnet publish "jewellery vertification project.csproj" -c Release -o /app/publish

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Set environment variables for production
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "jewellery vertification project.dll"]
