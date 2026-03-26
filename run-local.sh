#!/bin/bash
set -e

PROJECT_DIR="$(cd "$(dirname "$0")/Workshopmanagement" && pwd)"

echo "==> Cleaning..."
dotnet clean "$PROJECT_DIR/Workshopmanagement.csproj"

echo "==> Building..."
dotnet build "$PROJECT_DIR/Workshopmanagement.csproj" -c Debug

echo "==> Running..."
export ASPNETCORE_ENVIRONMENT=Development
export ASPNETCORE_URLS=http://localhost:5000
export WebClientCulture=it-IT
export MongoServerAddress=bigboi-pc.local
export MongoServerPort=27017
export MongoDatabase=WorkshopManager
export ApplicationName=WorkshopManagement
export AllowInsecureTls=false
export IsBeta=false
export ImageBasePath="$PROJECT_DIR/data/images"
export TemplateBasePath=Templates
export TemplateSavePath="$PROJECT_DIR/data/templates"

mkdir -p "$PROJECT_DIR/data/images" "$PROJECT_DIR/data/templates"

dotnet run --project "$PROJECT_DIR/Workshopmanagement.csproj" --no-build
