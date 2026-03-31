@echo off
setlocal

set "PROJECT_DIR=%~dp0Workshopmanagement"

echo =^> Cleaning...
dotnet clean "%PROJECT_DIR%\Workshopmanagement.csproj"
if errorlevel 1 exit /b %errorlevel%

echo =^> Building...
dotnet build "%PROJECT_DIR%\Workshopmanagement.csproj" -c Debug
if errorlevel 1 exit /b %errorlevel%

echo =^> Running...
set ASPNETCORE_ENVIRONMENT=Development
set ASPNETCORE_URLS=http://localhost:5000
set WebClientCulture=it-IT
set MongoServerAddress=bigboi-pc.local
set MongoServerPort=27017
set MongoDatabase=WorkshopManager
set ApplicationName=WorkshopManagement
set AllowInsecureTls=false
set IsBeta=false
set ImageBasePath=%PROJECT_DIR%\data\images
set TemplateBasePath=Templates

if not exist "%PROJECT_DIR%\data\images" mkdir "%PROJECT_DIR%\data\images"
if not exist "%PROJECT_DIR%\wwwroot\templates" mkdir "%PROJECT_DIR%\wwwroot\templates"

dotnet run --project "%PROJECT_DIR%\Workshopmanagement.csproj" --no-build
