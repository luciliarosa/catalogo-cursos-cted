#!/usr/bin/env bash
# ==============================================================================
# Inicializador Híbrido (macOS / Linux) — Catálogo de Cursos CTED SESI-SP
# ==============================================================================
set -e

ROOT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
cd "$ROOT_DIR"

# Garante permissões de leitura/escrita (corrige arquivos extraídos de ZIP do Windows no Mac)
chmod -R u+rwX "$ROOT_DIR/backend" "$ROOT_DIR/frontend" "$ROOT_DIR/scripts" 2>/dev/null || true

# Adiciona caminhos comuns do .NET no macOS ao PATH
export PATH="$HOME/.dotnet:/usr/local/share/dotnet:/opt/homebrew/bin:/usr/local/bin:$PATH"

echo "=================================================================="
echo "  SESI-SP CTED — Catálogo de Cursos (Modo Híbrido macOS / Windows)"
echo "=================================================================="

# Libera a porta 5298 caso já exista uma instância anterior rodando
if command -v lsof >/dev/null 2>&1; then
  EXISTING_PID=$(lsof -ti tcp:5298 || true)
  if [ -n "$EXISTING_PID" ]; then
    echo "[INFO] Liberando porta 5298 (PID anterior: $EXISTING_PID)..."
    kill -9 $EXISTING_PID 2>/dev/null || true
    sleep 1
  fi
fi

if command -v dotnet >/dev/null 2>&1 && dotnet --list-sdks 2>/dev/null | grep -q "^8\."; then
  echo "[MOTOR] .NET 8 SDK detectado ($(dotnet --version)). Iniciando via C# / ASP.NET Core..."
  cd "$ROOT_DIR/backend/SesiCursos.Api"
  exec dotnet run
elif command -v dotnet >/dev/null 2>&1 && dotnet --list-runtimes 2>/dev/null | grep -q "Microsoft.AspNetCore.App 8\."; then
  echo "[MOTOR] .NET 8 Runtime detectado. Iniciando binário pré-compilado..."
  cd "$ROOT_DIR/publish/backend"
  exec dotnet SesiCursos.Api.dll --urls "http://localhost:5298"
elif command -v python3 >/dev/null 2>&1; then
  echo "[MOTOR] Iniciando via Servidor Híbrido Multiplataforma (Python 3 + SQLite 3)..."
  exec python3 "$ROOT_DIR/scripts/hybrid_server.py" --port 5298
else
  echo "[ERRO] Nenhum runtime (.NET 8 ou Python 3) encontrado no sistema."
  exit 1
fi
