#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
#FROM mcr.microsoft.com/dotnet/aspnet:8.0.0-rc.2-bookworm-slim-arm64v8 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

#FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Workshopmanagement/Workshopmanagement.csproj", "Workshopmanagement/"]
RUN dotnet restore "Workshopmanagement/Workshopmanagement.csproj"
COPY . .
WORKDIR "/src/Workshopmanagement"
RUN dotnet build "Workshopmanagement.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Workshopmanagement.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Workshopmanagement.dll"]