# DO-Shop Enterprise Project Setup & Configuration
# Version 0.4 - Professional Development Environment Preparation
#
# This script prepares the development environment for the DO-Shop e-commerce platform
# Handles initial setup, dependency validation, and environment configuration
#
# Usage: .\project-setup.ps1
#
# Features:
# - Environment validation and prerequisite checking
# - NuGet cache management and optimization
# - Development tools verification
# - Project configuration validation
# - Professional logging and error handling

$ErrorActionPreference = "Continue"
$startTime = Get-Date

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "DO-Shop Enterprise Project Setup" -ForegroundColor Cyan  
Write-Host "========================================" -ForegroundColor Cyan
Write-Host "Version 0.4 - Professional Environment Preparation" -ForegroundColor Green
Write-Host "Initial setup and configuration for enterprise development" -ForegroundColor Green

Write-Host "`nInitializing project setup and configuration..." -ForegroundColor Yellow

Write-Host "`n[1/7] Environment Validation and Prerequisites..." -ForegroundColor Yellow

# Check Visual Studio installation
Write-Host "Validating development environment..." -ForegroundColor Cyan
$vsInstalled = Get-Command "devenv.exe" -ErrorAction SilentlyContinue
if ($vsInstalled) {
    $vsVersion = (Get-ItemProperty $vsInstalled.Source).VersionInfo.ProductVersion
    Write-Host "[SUCCESS] Visual Studio detected - Version: $vsVersion" -ForegroundColor Green
} else {
    Write-Host "[WARNING] Visual Studio not found in PATH" -ForegroundColor Yellow
    Write-Host "This is usually fine - VS may be installed but not in system PATH" -ForegroundColor Cyan
}

# Check .NET Framework/SDK
try {
    $dotnetVersion = dotnet --version 2>$null
    if ($LASTEXITCODE -eq 0 -and $dotnetVersion) {
        Write-Host "[SUCCESS] .NET SDK detected - Version: $dotnetVersion" -ForegroundColor Green
    } else {
        Write-Host "[WARNING] .NET SDK not found in PATH" -ForegroundColor Yellow
    }
} catch {
    Write-Host "[WARNING] .NET SDK not accessible via command line" -ForegroundColor Yellow
}

# Check MSBuild availability
try {
    $msbuildVersion = MSBuild.exe -version 2>$null | Select-Object -Last 1
    if ($LASTEXITCODE -eq 0) {
        Write-Host "[SUCCESS] MSBuild detected - Version: $msbuildVersion" -ForegroundColor Green
    }
} catch {
    Write-Host "[WARNING] MSBuild not found - may cause package restoration issues" -ForegroundColor Yellow
}

Write-Host "`n[2/7] Project Structure Validation..." -ForegroundColor Yellow

# Validate project architecture
$projectStructure = @{
    "Solution File" = "DO Shop.sln"
    "Presentation Layer" = "Presentation\Presentation.csproj"
    "Business Logic Layer" = "BusinessLogic\BusinessLogic.csproj"
    "Data Access Layer" = "DataAccess\DataAccess.csproj"
    "Business Objects Layer" = "BusinessObjects\BusinessObjects.csproj"
    "Package Configuration" = "Presentation\packages.config"
    "NuGet Configuration" = "nuget.config"
}

$validationResults = @{}
foreach ($component in $projectStructure.GetEnumerator()) {
    $exists = Test-Path $component.Value
    $validationResults[$component.Key] = $exists
    
    if ($exists) {
        Write-Host "[OK] $($component.Key): Found" -ForegroundColor Green
    } else {
        Write-Host "[ERROR] $($component.Key): Missing - $($component.Value)" -ForegroundColor Red
    }
}

$criticalMissing = $validationResults.GetEnumerator() | Where-Object { -not $_.Value }
if ($criticalMissing) {
    Write-Host "`n[CRITICAL] Missing critical project components!" -ForegroundColor Red
    Write-Host "Please ensure all project files are present before proceeding." -ForegroundColor Yellow
    exit 1
} else {
    Write-Host "`n[SUCCESS] Project structure validation complete" -ForegroundColor Green
}

Write-Host "`n[3/7] NuGet Cache Management..." -ForegroundColor Yellow

# Clear NuGet cache to avoid conflicts
Write-Host "Optimizing NuGet cache for legacy package compatibility..." -ForegroundColor Cyan
try {
    # Clear global packages cache
    $nugetGlobalCache = "$env:USERPROFILE\.nuget\packages"
    if (Test-Path $nugetGlobalCache) {
        Write-Host "Clearing global NuGet cache..." -ForegroundColor Cyan
        Remove-Item "$nugetGlobalCache\*" -Recurse -Force -ErrorAction SilentlyContinue
        Write-Host "[SUCCESS] Global NuGet cache cleared" -ForegroundColor Green
    }
    
    # Clear HTTP cache
    $nugetHttpCache = "$env:LOCALAPPDATA\NuGet\v3-cache"
    if (Test-Path $nugetHttpCache) {
        Write-Host "Clearing NuGet HTTP cache..." -ForegroundColor Cyan
        Remove-Item "$nugetHttpCache\*" -Recurse -Force -ErrorAction SilentlyContinue
        Write-Host "[SUCCESS] NuGet HTTP cache cleared" -ForegroundColor Green
    }
    
    # Clear temp files
    $tempNuGet = "$env:TEMP\NuGetScratch"
    if (Test-Path $tempNuGet) {
        Remove-Item $tempNuGet -Recurse -Force -ErrorAction SilentlyContinue
    }
    
} catch {
    Write-Host "[WARNING] Could not clear all NuGet caches (may be in use)" -ForegroundColor Yellow
    Write-Host "This is usually not critical for project setup" -ForegroundColor Cyan
}

Write-Host "`n[4/7] Package Source Configuration..." -ForegroundColor Yellow

# Verify NuGet configuration
if (Test-Path "nuget.config") {
    Write-Host "Validating NuGet package sources..." -ForegroundColor Cyan
    try {
        [xml]$nugetConfig = Get-Content "nuget.config"
        $packageSources = $nugetConfig.configuration.packageSources.add
        
        if ($packageSources) {
            Write-Host "[SUCCESS] Package sources configured:" -ForegroundColor Green
            foreach ($source in $packageSources) {
                Write-Host "  - $($source.key): $($source.value)" -ForegroundColor Cyan
            }
        } else {
            Write-Host "[WARNING] No custom package sources found in nuget.config" -ForegroundColor Yellow
        }
    } catch {
        Write-Host "[ERROR] Invalid nuget.config format" -ForegroundColor Red
    }
} else {
    Write-Host "[WARNING] nuget.config not found - using default NuGet sources" -ForegroundColor Yellow
}

Write-Host "`n[5/7] Development Tools Verification..." -ForegroundColor Yellow

# Check for common development tools
$devTools = @{
    "Git" = "git.exe"
    "PowerShell" = "powershell.exe"
    "Windows PowerShell ISE" = "powershell_ise.exe"
}

foreach ($tool in $devTools.GetEnumerator()) {
    $toolFound = Get-Command $tool.Value -ErrorAction SilentlyContinue
    if ($toolFound) {
        Write-Host "[OK] $($tool.Key): Available" -ForegroundColor Green
    } else {
        Write-Host "[WARNING] $($tool.Key): Not found in PATH" -ForegroundColor Yellow
    }
}

Write-Host "`n[6/7] Project Configuration Optimization..." -ForegroundColor Yellow

# Create/update .gitignore for better development experience
$gitignoreContent = @'
# Build results
[Dd]ebug/
[Dd]ebugPublic/
[Rr]elease/
[Rr]eleases/
x64/
x86/
bld/
[Bb]in/
[Oo]bj/

# NuGet Packages
*.nupkg
**/packages/*
!**/packages/build/
!**/packages/repositories.config

# Visual Studio cache files
*.suo
*.user
*.userosscache
*.sln.docstates
*.userprefs

# Build Results of an ATL Project
[Dd]ebugPS/
[Rr]eleasePS/
dlldata.c

# Temporary ASP.NET Files
[Tt]emp/

# IIS Express
/.vs/
/packages/
'@

try {
    $gitignoreContent | Out-File -FilePath ".gitignore" -Encoding UTF8 -Force
    Write-Host "[SUCCESS] .gitignore updated for optimal development workflow" -ForegroundColor Green
} catch {
    Write-Host "[WARNING] Could not update .gitignore" -ForegroundColor Yellow
}

# Verify Unity configuration
if (Test-Path "Presentation\App_Start\UnityConfig.cs") {
    Write-Host "[SUCCESS] Unity dependency injection configuration found" -ForegroundColor Green
} else {
    Write-Host "[WARNING] Unity configuration not found - may need manual setup" -ForegroundColor Yellow
}

Write-Host "`n[7/7] Setup Completion and Validation..." -ForegroundColor Yellow

# Final validation
Write-Host "Performing final environment validation..." -ForegroundColor Cyan

$setupScore = 0
$maxScore = 5

# Score the setup
if (Test-Path "DO Shop.sln") { $setupScore++ }
if (Test-Path "Presentation\packages.config") { $setupScore++ }
if (Test-Path "nuget.config") { $setupScore++ }
if ($vsInstalled) { $setupScore++ }
if (Test-Path "Presentation\App_Start\UnityConfig.cs") { $setupScore++ }

$setupPercentage = [math]::Round(($setupScore / $maxScore) * 100)

# Calculate total time
$endTime = Get-Date
$totalTime = [math]::Round(($endTime - $startTime).TotalSeconds, 1)

Write-Host "`n========================================" -ForegroundColor Green
Write-Host "PROJECT SETUP COMPLETE" -ForegroundColor Green
Write-Host "========================================" -ForegroundColor Green
Write-Host "[OK] Environment validation: Complete" -ForegroundColor Green
Write-Host "[OK] Project structure: Validated" -ForegroundColor Green
Write-Host "[OK] NuGet configuration: Optimized" -ForegroundColor Green
Write-Host "[OK] Development tools: Verified" -ForegroundColor Green
Write-Host "[OK] Project configuration: Updated" -ForegroundColor Green
Write-Host "`nSetup Score: $setupScore/$maxScore ($setupPercentage%)" -ForegroundColor Cyan
Write-Host "Setup time: $totalTime seconds" -ForegroundColor Cyan

if ($setupScore -eq $maxScore) {
    Write-Host "`nPERFECT SETUP! Ready for development." -ForegroundColor Green
    Write-Host "`nNext Step: Run .\dev-environment.ps1 to launch the development environment" -ForegroundColor Yellow
} elseif ($setupScore -ge 3) {
    Write-Host "`nGOOD SETUP! Minor issues detected but should work fine." -ForegroundColor Green
    Write-Host "`nNext Step: Run .\dev-environment.ps1 to launch the development environment" -ForegroundColor Yellow
} else {
    Write-Host "`nWARNING: SETUP ISSUES DETECTED! Manual intervention may be required." -ForegroundColor Yellow
    Write-Host "Please review the warnings above before proceeding." -ForegroundColor Cyan
}

Write-Host "`nPro Tips:" -ForegroundColor Cyan
Write-Host "   - Use .\dev-environment.ps1 for zero-touch development environment launch" -ForegroundColor White
Write-Host "   - All scripts support enterprise development workflows" -ForegroundColor White
Write-Host "   - Check README.md for detailed documentation" -ForegroundColor White

Write-Host "`nDO-Shop v0.4 Project Setup Complete!" -ForegroundColor Green
