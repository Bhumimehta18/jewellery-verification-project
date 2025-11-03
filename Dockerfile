# Base SDK image
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# ✅ Use correct path to .csproj
COPY ["JewelleryVerificationProject/JewelleryVerificationProject.csproj", "JewelleryVerificationProject/"]

RUN dotnet restore "JewelleryVerificationProject/JewelleryVerificationProject.csproj"
COPY . .
WORKDIR "/src/JewelleryVerificationProject"
RUN dotnet build "JewelleryVerificationProject.csproj" -c Release -o /app/build
RUN dotnet publish "JewelleryVerificationProject.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "JewelleryVerificationProject.dll"]
