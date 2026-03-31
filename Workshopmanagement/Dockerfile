# https://aka.ms/customizecontainer

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

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

# Install Liberation Sans fonts for PDF generation
RUN apt-get update && apt-get install -y --no-install-recommends fonts-liberation tzdata && rm -rf /var/lib/apt/lists/*

# Create directories for persistent data
RUN mkdir -p /app/data/images /app/wwwroot/templates

ENTRYPOINT ["dotnet", "Workshopmanagement.dll"]