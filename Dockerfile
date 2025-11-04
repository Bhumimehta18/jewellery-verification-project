# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy the csproj and restore
COPY ["JewelleryVerificationProject.csproj", "./"]
RUN dotnet restore "JewelleryVerificationProject.csproj"

# Copy everything else and build
COPY . .
RUN dotnet publish "JewelleryVerificationProject.csproj" -c Release -o /app/publish

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "JewelleryVerificationProject.dll"]
