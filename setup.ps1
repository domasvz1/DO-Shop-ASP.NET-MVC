# DO-Shop Setup Script
# Run this script to set up the project for development

Write-Host "Setting up DO-Shop project..." -ForegroundColor Green

# Clear NuGet cache to avoid conflicts
Write-Host "Clearing NuGet cache..." -ForegroundColor Yellow
dotnet nuget locals all --clear

# Restore packages
Write-Host "Restoring NuGet packages..." -ForegroundColor Yellow
nuget restore "DO Shop.sln"

# Build the solution
Write-Host "Building solution..." -ForegroundColor Yellow
msbuild "DO Shop.sln" /p:Configuration=Debug

Write-Host "Setup complete! You can now:" -ForegroundColor Green
Write-Host "1. Set 'Presentation' as startup project in Visual Studio" -ForegroundColor White
Write-Host "2. Run database migrations (see README.md)" -ForegroundColor White
Write-Host "3. Press F5 to run the application" -ForegroundColor White
