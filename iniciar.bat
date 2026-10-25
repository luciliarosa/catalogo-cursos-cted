@echo off
chcp 65001 >nul
REM ============================================================================
REM Inicializador Hibrido (Windows CMD) — Catalogo de Cursos CTED SESI-SP
REM ============================================================================
setlocal
cd /d "%~dp0"

echo ==================================================================
echo   SESI-SP CTED - Catalogo de Cursos (Modo Hibrido Windows / macOS)
echo ==================================================================

where dotnet >nul 2>nul
if %ERRORLEVEL% EQU 0 (
    echo [MOTOR] .NET SDK detectado. Iniciando via C# / ASP.NET Core na porta 5298...
    cd /d "%~dp0backend\SesiCursos.Api"
    dotnet run
    goto :eof
)

where python >nul 2>nul
if %ERRORLEVEL% EQU 0 (
    echo [MOTOR] .NET nao encontrado no PATH. Iniciando via Servidor Hibrido ^(Python + SQLite^) na porta 5298...
    python "%~dp0scripts\hybrid_server.py" --port 5298
    goto :eof
)

where py >nul 2>nul
if %ERRORLEVEL% EQU 0 (
    echo [MOTOR] .NET nao encontrado no PATH. Iniciando via Servidor Hibrido ^(Python + SQLite^) na porta 5298...
    py -3 "%~dp0scripts\hybrid_server.py" --port 5298
    goto :eof
)

echo [ERRO] Instale o .NET 8 SDK ou Python 3 para iniciar o servidor.
pause
