#!/usr/bin/env bash
# ==============================================================================
# Executor de Testes Híbrido (macOS / Linux) — Catálogo de Cursos CTED SESI-SP
# ==============================================================================
set -e

ROOT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
cd "$ROOT_DIR"

export PATH="$HOME/.dotnet:/usr/local/share/dotnet:/opt/homebrew/bin:/usr/local/bin:$PATH"

if command -v dotnet >/dev/null 2>&1 && dotnet --list-sdks 2>/dev/null | grep -q "^8\."; then
  echo "[TESTES] Executando xUnit via .NET 8 SDK..."
  dotnet test "$ROOT_DIR/backend/SesiCursos.Tests/SesiCursos.Tests.csproj"
else
  echo "[TESTES] Executando suite de 11 testes de integração via Motor Híbrido (Python 3 + SQLite)..."
  python3 "$ROOT_DIR/scripts/hybrid_server.py" --test
fi
