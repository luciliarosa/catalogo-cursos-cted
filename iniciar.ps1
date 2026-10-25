#!/usr/bin/env pwsh
# ==============================================================================
# Inicializador Híbrido (PowerShell Windows / macOS) — Catálogo CTED SESI-SP
# ==============================================================================
$ErrorActionPreference = "Stop"
$RootDir = Split-Path -Parent $MyInvocation.MyCommand.Path
Set-Location $RootDir

if ($IsMacOS -or $IsLinux) {
    $env:PATH = "$HOME/.dotnet:/usr/local/share/dotnet:/opt/homebrew/bin:/usr/local/bin:$env:PATH"
    chmod -R u+rwX "$RootDir/backend" "$RootDir/frontend" "$RootDir/scripts" 2>$null
}

Write-Host "==================================================================" -ForegroundColor Cyan
Write-Host "  SESI-SP CTED — Catálogo de Cursos (Modo Híbrido Windows / macOS)" -ForegroundColor Cyan
Write-Host "==================================================================" -ForegroundColor Cyan

if (Get-Command dotnet -ErrorAction SilentlyContinue) {
    Write-Host "[MOTOR] .NET SDK detectado. Iniciando API C# na porta 5298..." -ForegroundColor Green
    Set-Location (Join-Path $RootDir "backend/SesiCursos.Api")
    dotnet run
} elseif (Get-Command python3 -ErrorAction SilentlyContinue) {
    Write-Host "[MOTOR] Iniciando Servidor Híbrido (Python 3 + SQLite) na porta 5298..." -ForegroundColor Yellow
    python3 (Join-Path $RootDir "scripts/hybrid_server.py") --port 5298
} elseif (Get-Command python -ErrorAction SilentlyContinue) {
    Write-Host "[MOTOR] Iniciando Servidor Híbrido (Python + SQLite) na porta 5298..." -ForegroundColor Yellow
    python (Join-Path $RootDir "scripts/hybrid_server.py") --port 5298
} else {
    Write-Error "Nenhum runtime (.NET 8 ou Python 3) encontrado."
}
