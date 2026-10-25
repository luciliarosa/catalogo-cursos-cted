@echo off
chcp 65001 >nul
setlocal
cd /d "%~dp0"

where dotnet >nul 2>nul
if %ERRORLEVEL% EQU 0 (
    echo [TESTES] Executando xUnit via .NET SDK...
    dotnet test "%~dp0backend\SesiCursos.Tests\SesiCursos.Tests.csproj"
    goto :eof
)

where python >nul 2>nul
if %ERRORLEVEL% EQU 0 (
    echo [TESTES] Executando suite de 11 testes de integracao via Motor Hibrido...
    python "%~dp0scripts\hybrid_server.py" --test
    goto :eof
)
