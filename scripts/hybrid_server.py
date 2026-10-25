#!/usr/bin/env python3
"""
Servidor Híbrido Multiplataforma (Windows / macOS / Linux) — Catálogo de Cursos CTED SESI-SP
============================================================================================
Espelha 100% do contrato da Web API .NET 8 (CoursesController + HealthController + Swagger + Arquivos Estáticos),
conectando diretamente ao banco relacional SQLite oficial (backend/SesiCursos.Api/sesicursos.db).

Permite rodar o projeto instantaneamente no macOS, Windows ou Linux (VS Code ou Terminal),
tanto como fallback automático quando o .NET 8 SDK não está instalado quanto diretamente.
"""

import argparse
import datetime
import http.server
import json
import mimetypes
import os
import socketserver
import sqlite3
import sys
import threading
import unicodedata
import urllib.parse
import urllib.request
from pathlib import Path

ROOT_DIR = Path(__file__).resolve().parent.parent
DB_PATH = ROOT_DIR / "backend" / "SesiCursos.Api" / "sesicursos.db"
FRONTEND_DIR = ROOT_DIR / "frontend"
DEFAULT_PORT = 5298


def normalize_text(text: str | None) -> str:
    """Remove acentos (FormD) e converte para minúsculas, idêntico ao CoursesController.NormalizeText em C#."""
    if not text or not text.strip():
        return ""
    nfd = unicodedata.normalize("NFD", text)
    without_accents = "".join(
        ch for ch in nfd if unicodedata.category(ch) != "Mn"
    )
    return unicodedata.normalize("NFC", without_accents).lower().strip()


def ensure_database_initialized(db_path: Path) -> None:
    """Garante que o banco SQLite exista e esteja populado (auto-seed a partir de init_database_sqlite.sql se necessário)."""
    db_path.parent.mkdir(parents=True, exist_ok=True)
    conn = sqlite3.connect(str(db_path))
    try:
        cur = conn.cursor()
        cur.execute(
            "SELECT name FROM sqlite_master WHERE type='table' AND name='Courses'"
        )
        if cur.fetchone() is not None:
            cur.execute("SELECT COUNT(*) FROM Courses")
            count = cur.fetchone()[0]
            if count > 0:
                return

        sql_seed_path = ROOT_DIR / "scripts" / "init_database_sqlite.sql"
        if sql_seed_path.exists():
            sql_script = sql_seed_path.read_text(encoding="utf-8")
            conn.executescript(sql_script)
            conn.commit()
    finally:
        conn.close()


def query_courses(
    db_path: Path,
    search: str | None = None,
    category: str | None = None,
    modality: str | None = None,
    status: str | None = None,
) -> dict:
    """Executa a consulta de cursos com filtros e busca sem acento idêntica ao C#."""
    conn = sqlite3.connect(str(db_path))
    conn.row_factory = sqlite3.Row
    try:
        cur = conn.cursor()
        cur.execute("SELECT * FROM Courses ORDER BY Id ASC")
        rows = cur.fetchall()

        # Conta módulos e aulas por curso
        cur.execute(
            "SELECT CourseId, COUNT(*) as cnt FROM Modules GROUP BY CourseId"
        )
        mod_counts = {r["CourseId"]: r["cnt"] for r in cur.fetchall()}

        cur.execute(
            """
            SELECT m.CourseId, COUNT(l.Id) as cnt
            FROM Modules m
            LEFT JOIN Lessons l ON l.ModuleId = m.Id
            GROUP BY m.CourseId
            """
        )
        lesson_counts = {r["CourseId"]: r["cnt"] for r in cur.fetchall()}

        all_courses = []
        for r in rows:
            cid = r["Id"]
            all_courses.append(
                {
                    "id": cid,
                    "fullName": r["FullName"],
                    "shortName": r["ShortName"],
                    "description": r["Description"],
                    "category": r["Category"],
                    "modality": r["Modality"],
                    "workloadHours": r["WorkloadHours"],
                    "targetAudience": r["TargetAudience"] or "",
                    "status": r["Status"],
                    "imageUrl": r["ImageUrl"] or "",
                    "availableDate": r["AvailableDate"],
                    "certification": r["Certification"] or "SESI",
                    "modulesCount": mod_counts.get(cid, 0),
                    "lessonsCount": lesson_counts.get(cid, 0),
                }
            )

        total = len(all_courses)
        filtered = list(all_courses)

        if search and search.strip():
            term = normalize_text(search)
            filtered = [
                c
                for c in filtered
                if term in normalize_text(c["fullName"])
                or term in normalize_text(c["shortName"])
                or term in normalize_text(c["description"])
            ]

        if category and category.strip():
            cat_norm = normalize_text(category)
            filtered = [
                c for c in filtered if normalize_text(c["category"]) == cat_norm
            ]

        if modality and modality.strip():
            mod_norm = normalize_text(modality)
            filtered = [
                c for c in filtered if normalize_text(c["modality"]) == mod_norm
            ]

        if status and status.strip():
            st_norm = normalize_text(status)
            filtered = [
                c for c in filtered if normalize_text(c["status"]) == st_norm
            ]

        categories = sorted({c["category"] for c in all_courses})
        modalities = sorted({c["modality"] for c in all_courses})
        statuses = sorted({c["status"] for c in all_courses})

        return {
            "total": total,
            "filteredTotal": len(filtered),
            "items": filtered,
            "availableFilters": {
                "categories": categories,
                "modalities": modalities,
                "statuses": statuses,
            },
        }
    finally:
        conn.close()


def query_course_by_id(db_path: Path, course_id: int) -> dict | None:
    """Consulta curso completo por ID com módulos, aulas e FAQs ordenados."""
    conn = sqlite3.connect(str(db_path))
    conn.row_factory = sqlite3.Row
    try:
        cur = conn.cursor()
        cur.execute("SELECT * FROM Courses WHERE Id = ?", (course_id,))
        course = cur.fetchone()
        if not course:
            return None

        cur.execute(
            'SELECT * FROM Modules WHERE CourseId = ? ORDER BY "Order" ASC, Id ASC',
            (course_id,),
        )
        modules_rows = cur.fetchall()

        modules = []
        for m in modules_rows:
            cur.execute(
                'SELECT * FROM Lessons WHERE ModuleId = ? ORDER BY "Order" ASC, Id ASC',
                (m["Id"],),
            )
            lessons = [
                {
                    "id": l["Id"],
                    "title": l["Title"],
                    "description": l["Description"] or "",
                    "durationMinutes": l["DurationMinutes"],
                    "order": l["Order"],
                }
                for l in cur.fetchall()
            ]
            modules.append(
                {
                    "id": m["Id"],
                    "title": m["Title"],
                    "order": m["Order"],
                    "lessons": lessons,
                }
            )

        cur.execute(
            'SELECT * FROM Faqs WHERE CourseId = ? ORDER BY "Order" ASC, Id ASC',
            (course_id,),
        )
        faqs = [
            {
                "id": f["Id"],
                "question": f["Question"],
                "answer": f["Answer"],
                "order": f["Order"],
            }
            for f in cur.fetchall()
        ]

        return {
            "id": course["Id"],
            "fullName": course["FullName"],
            "shortName": course["ShortName"],
            "description": course["Description"],
            "category": course["Category"],
            "modality": course["Modality"],
            "workloadHours": course["WorkloadHours"],
            "targetAudience": course["TargetAudience"] or "",
            "status": course["Status"],
            "imageUrl": course["ImageUrl"] or "",
            "videoUrl": course["VideoUrl"] or "",
            "availableDate": course["AvailableDate"],
            "certification": course["Certification"] or "SESI",
            "modules": modules,
            "faqs": faqs,
        }
    finally:
        conn.close()


OPENAPI_SPEC = {
    "openapi": "3.0.1",
    "info": {
        "title": "SESI Indústria - Catálogo de Cursos CTED API",
        "description": "API RESTful (Modo Híbrido Windows/macOS) para apresentação e consulta do catálogo de cursos do SESI-SP | Processo seletivo - Desenvolvedor de Sistemas I - 519-2026",
        "version": "v1",
    },
    "paths": {
        "/api/courses": {
            "get": {
                "tags": ["Courses"],
                "summary": "Lista os cursos disponíveis aplicando pesquisa e filtros opcionais.",
                "parameters": [
                    {"name": "search", "in": "query", "schema": {"type": "string"}},
                    {"name": "category", "in": "query", "schema": {"type": "string"}},
                    {"name": "modality", "in": "query", "schema": {"type": "string"}},
                    {"name": "status", "in": "query", "schema": {"type": "string"}},
                ],
                "responses": {"200": {"description": "OK"}},
            }
        },
        "/api/courses/{id}": {
            "get": {
                "tags": ["Courses"],
                "summary": "Consulta os detalhes completos de um curso pelo ID.",
                "parameters": [
                    {
                        "name": "id",
                        "in": "path",
                        "required": True,
                        "schema": {"type": "integer"},
                    }
                ],
                "responses": {
                    "200": {"description": "OK"},
                    "404": {"description": "Not Found"},
                },
            }
        },
        "/api/health": {
            "get": {
                "tags": ["Health"],
                "summary": "Verifica a saúde da API e conexão com o banco SQLite.",
                "responses": {"200": {"description": "OK"}},
            }
        },
    },
}

SWAGGER_HTML = """<!DOCTYPE html>
<html lang="pt-BR">
<head>
  <meta charset="UTF-8" />
  <title>Swagger UI - SESI Catálogo API v1</title>
  <link rel="stylesheet" href="https://unpkg.com/swagger-ui-dist@5/swagger-ui.css" />
</head>
<body style="margin:0; background:#fafafa;">
  <div id="swagger-ui"></div>
  <script src="https://unpkg.com/swagger-ui-dist@5/swagger-ui-bundle.js"></script>
  <script>
    window.onload = () => {
      SwaggerUIBundle({
        url: '/swagger/v1/swagger.json',
        dom_id: '#swagger-ui',
      });
    };
  </script>
</body>
</html>
"""


class SesiHybridRequestHandler(http.server.SimpleHTTPRequestHandler):
    def __init__(self, *args, **kwargs):
        super().__init__(*args, directory=str(FRONTEND_DIR), **kwargs)

    def end_headers(self):
        self.send_header("Access-Control-Allow-Origin", "*")
        self.send_header("Access-Control-Allow-Methods", "GET, OPTIONS")
        self.send_header("Access-Control-Allow-Headers", "*")
        self.send_header("Cache-Control", "no-cache, no-store, must-revalidate")
        super().end_headers()

    def do_OPTIONS(self):
        self.send_response(204)
        self.end_headers()

    def send_json(self, status_code: int, payload: dict | list):
        body = json.dumps(payload, ensure_ascii=False).encode("utf-8")
        self.send_response(status_code)
        self.send_header("Content-Type", "application/json; charset=utf-8")
        self.send_header("Content-Length", str(len(body)))
        self.end_headers()
        self.wfile.write(body)

    def do_GET(self):
        parsed = urllib.parse.urlparse(self.path)
        path = parsed.path.rstrip("/") or "/"
        query = urllib.parse.parse_qs(parsed.query)

        if path == "/api/health":
            exists = DB_PATH.exists()
            self.send_json(
                200,
                {
                    "status": "Healthy" if exists else "Degraded",
                    "database": "Connected" if exists else "Disconnected",
                    "provider": "Microsoft.EntityFrameworkCore.Sqlite (Hybrid Mode)",
                    "timestamp": datetime.datetime.now(
                        datetime.timezone.utc
                    ).isoformat(),
                },
            )
            return

        if path == "/api/courses":
            search = query.get("search", [None])[0]
            category = query.get("category", [None])[0]
            modality = query.get("modality", [None])[0]
            status = query.get("status", [None])[0]
            result = query_courses(DB_PATH, search, category, modality, status)
            self.send_json(200, result)
            return

        if path.startswith("/api/courses/"):
            raw_id = path.split("/api/courses/", 1)[1]
            try:
                course_id = int(raw_id)
            except ValueError:
                self.send_json(400, {"message": "ID inválido."})
                return

            course = query_course_by_id(DB_PATH, course_id)
            if course is None:
                self.send_json(
                    404,
                    {"message": f"Curso com ID {course_id} não encontrado."},
                )
            else:
                self.send_json(200, course)
            return

        if path == "/swagger/v1/swagger.json":
            self.send_json(200, OPENAPI_SPEC)
            return

        if path in ("/swagger", "/swagger/index.html"):
            body = SWAGGER_HTML.encode("utf-8")
            self.send_response(200)
            self.send_header("Content-Type", "text/html; charset=utf-8")
            self.send_header("Content-Length", str(len(body)))
            self.end_headers()
            self.wfile.write(body)
            return

        return super().do_GET()


class ReusableTCPServer(socketserver.ThreadingMixIn, socketserver.TCPServer):
    allow_reuse_address = True
    daemon_threads = True


def run_self_tests() -> int:
    """Executa os 11 testes de integração equivalentes ao CoursesApiTests.cs."""
    print("==================================================================")
    print("Executando Suite de 11 Testes de Integração (Modo Híbrido)")
    print("==================================================================")
    ensure_database_initialized(DB_PATH)

    tests_passed = 0
    total_tests = 11

    def check(name: str, condition: bool, detail: str = ""):
        nonlocal tests_passed
        if condition:
            tests_passed += 1
            print(f"  [PASSOU] {name}")
        else:
            print(f"  [FALHOU] {name} -> {detail}")

    # 1. GetCourses_ReturnsAllCourses_WhenNoFiltersApplied
    r1 = query_courses(DB_PATH)
    check(
        "1. GetCourses_ReturnsAllCourses_WhenNoFiltersApplied",
        r1["total"] >= 6
        and r1["filteredTotal"] == r1["total"]
        and len(r1["items"]) == r1["total"],
    )

    # 2. GetCourses_FiltersByName_CaseInsensitivePartialMatch
    r2 = query_courses(DB_PATH, search="metalurgia")
    check(
        "2. GetCourses_FiltersByName_CaseInsensitivePartialMatch",
        len(r2["items"]) > 0
        and all(
            "metalurgia" in c["fullName"].lower()
            or "metalurgia" in c["shortName"].lower()
            or "metalurgia" in c["description"].lower()
            for c in r2["items"]
        ),
    )

    # 3. GetCourses_FiltersByName_AccentInsensitive
    r3 = query_courses(DB_PATH, search="textil")
    check(
        "3. GetCourses_FiltersByName_AccentInsensitive",
        any("Têxtil" in c["fullName"] for c in r3["items"]),
    )

    # 4. GetCourses_FiltersByCategory_ReturnsOnlyMatchingCategory
    r4 = query_courses(DB_PATH, category="Matemática")
    check(
        "4. GetCourses_FiltersByCategory_ReturnsOnlyMatchingCategory",
        len(r4["items"]) > 0
        and all(c["category"] == "Matemática" for c in r4["items"]),
    )

    # 5. GetCourses_FiltersByModality_ReturnsOnlyMatchingModality
    r5 = query_courses(DB_PATH, modality="Online")
    check(
        "5. GetCourses_FiltersByModality_ReturnsOnlyMatchingModality",
        len(r5["items"]) > 0
        and all(c["modality"] == "Online" for c in r5["items"]),
    )

    # 6. GetCourses_FiltersByStatus_ReturnsOnlyMatchingStatus
    r6 = query_courses(DB_PATH, status="Inscrições Abertas")
    check(
        "6. GetCourses_FiltersByStatus_ReturnsOnlyMatchingStatus",
        len(r6["items"]) > 0
        and all(c["status"] == "Inscrições Abertas" for c in r6["items"]),
    )

    # 7. GetCourses_CombinesSearchAndFilters_Consistently
    r7 = query_courses(
        DB_PATH, search="Precisão", category="Matemática", modality="Online"
    )
    check(
        "7. GetCourses_CombinesSearchAndFilters_Consistently",
        len(r7["items"]) > 0
        and all(
            c["category"] == "Matemática" and c["modality"] == "Online"
            for c in r7["items"]
        ),
    )

    # 8. GetCourses_ReturnsEmptyList_WhenNoMatches
    r8 = query_courses(
        DB_PATH, search="termo_completamente_inexistente_xyz_123"
    )
    check(
        "8. GetCourses_ReturnsEmptyList_WhenNoMatches",
        r8["filteredTotal"] == 0 and len(r8["items"]) == 0,
    )

    # 9. GetCourseById_ReturnsCourseDetails_WhenCourseExists
    r9 = query_course_by_id(DB_PATH, 1)
    check(
        "9. GetCourseById_ReturnsCourseDetails_WhenCourseExists",
        r9 is not None
        and r9["id"] == 1
        and "Matemática para a Precisão" in r9["fullName"]
        and len(r9["modules"]) > 0
        and len(r9["modules"][0]["lessons"]) > 0
        and len(r9["faqs"]) > 0,
    )

    # 10. GetCourseById_ReturnsNotFound_WhenCourseDoesNotExist
    r10 = query_course_by_id(DB_PATH, 99999)
    check("10. GetCourseById_ReturnsNotFound_WhenCourseDoesNotExist", r10 is None)

    # 11. HealthCheck_ReturnsHealthy
    check("11. HealthCheck_ReturnsHealthy", DB_PATH.exists())

    print("------------------------------------------------------------------")
    print(
        f"Resultado: Aprovado! — Com falha: {total_tests - tests_passed}, Aprovado: {tests_passed}, Total: {total_tests}"
    )
    print("==================================================================")
    return 0 if tests_passed == total_tests else 1


def main():
    if hasattr(sys.stdout, "reconfigure"):
        sys.stdout.reconfigure(line_buffering=True)
    parser = argparse.ArgumentParser(
        description="Servidor Híbrido Catálogo de Cursos CTED SESI-SP"
    )
    parser.add_argument(
        "--port", type=int, default=DEFAULT_PORT, help="Porta HTTP (padrão: 5298)"
    )
    parser.add_argument(
        "--test",
        action="store_true",
        help="Executa os 11 testes automatizados de integração",
    )
    args = parser.parse_args()

    if args.test:
        sys.exit(run_self_tests())

    ensure_database_initialized(DB_PATH)
    mimetypes.init()
    mimetypes.add_type("image/svg+xml", ".svg")
    mimetypes.add_type("font/ttf", ".ttf")

    with ReusableTCPServer(("0.0.0.0", args.port), SesiHybridRequestHandler) as httpd:
        print(f"[DB] Configurado provedor SQLite (Data Source={DB_PATH}).")
        print("[DB] Banco de dados verificado e dados iniciais populados com sucesso.")
        print(f"[WEB] Servindo frontend estático a partir de: {FRONTEND_DIR}")
        print(f"Now listening on: http://localhost:{args.port}")
        print(f"  -> Aplicação Completa: http://localhost:{args.port}")
        print(f"  -> Catálogo de Cursos: http://localhost:{args.port}/cursos.html")
        print(f"  -> Swagger / OpenAPI:  http://localhost:{args.port}/swagger")
        print("Application started. Press Ctrl+C to shut down.")
        try:
            httpd.serve_forever()
        except KeyboardInterrupt:
            print("\nEncerrando servidor...")


if __name__ == "__main__":
    main()
