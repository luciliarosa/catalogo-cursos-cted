# Catálogo de Cursos CTED — SESI-SP

Projeto desenvolvido para o processo seletivo de **Desenvolvedor de Sistemas I (Edital 519/2026)** do **SESI-SP (CTED)**.

## Links de Entrega

- **Link da Aplicação (Render):** 
- **Link do Swagger (API):** 
- **Link do Repositório (GitHub):** 

> **Observação sobre o plano gratuito do Render:** Como a aplicação está hospedada no plano gratuito do Render, o servidor entra em modo de espera após alguns minutos sem acesso. Por isso, **no primeiro acesso pode levar cerca de 40 a 50 segundos** para o servidor acordar e carregar os cursos. Nos acessos seguintes o carregamento é imediato.

---

## 1. Sobre o Projeto

O objetivo deste projeto é apresentar o catálogo de cursos do Centro de Tecnologias e Educação Digital (CTED) do SESI-SP, seguindo o protótipo disponibilizado no Figma.

A aplicação possui:
- **Página inicial (`index.html`)**: banner principal, carrossel com os principais cursos, seção de números/impacto, depoimentos e chamada para matrícula.
- **Catálogo de cursos (`cursos.html`)**: listagem dos cursos vinda da API, contador de resultados, busca por nome (funcionando com ou sem acento), filtros por categoria, modalidade e status, além do botão para limpar os filtros.
- **Detalhes do curso (`detalhes.html`)**: informações completas carregadas pelo ID do curso, vídeo de capa, diferenciais, descrição "Sobre o curso", sanfonas (Módulos, Aulas online e avaliações, Dúvidas frequentes), depoimentos e barra inferior de matrícula.
- **Ambiente Virtual de Aprendizagem (`ava.html`)**: tela do aluno com barra de progresso, busca lateral por módulos/aulas e conteúdo de boas práticas e recomendações.

## 2. Tecnologias Utilizadas

**Back-end:**
- C# com .NET 8 (ASP.NET Core Web API)
- Entity Framework Core 8
- Swagger (para testar e visualizar as rotas da API)
- xUnit (para os testes automatizados da API)

**Banco de Dados:**
- **SQLite** (`sesicursos.db`) configurado por padrão para rodar localmente sem precisar instalar servidor de banco de dados.
- **PostgreSQL** suportado via string de conexão (`DATABASE_URL` ou `appsettings.json`), com script SQL na pasta `scripts/init_database_postgres.sql`.

**Front-end:**
- HTML5, CSS3 e JavaScript puro (Vanilla JS)
- Fonte Open Sans e ícones/imagens exportados do Figma

## 3. Como a Aplicação Funciona (Arquitetura)

Separei o projeto em duas pastas principais (`frontend` e `backend`):

1. O **front-end** faz requisições `fetch` (GET) para a API na porta `5298` (arquivo `frontend/js/api.js`).
2. O **back-end** recebe as chamadas no `CoursesController`, consulta o banco de dados pelo Entity Framework (`AppDbContext`) e devolve os dados em JSON usando DTOs.
3. No `Program.cs`, configurei o `UseStaticFiles` para que o próprio servidor da API também sirva os arquivos da pasta `frontend/`. Assim dá para abrir tudo direto em `http://localhost:5298`, ou se preferir, abrir o HTML separado pelo Live Server do VS Code.

## 4. Estrutura de Pastas

```text
catalogo-cursos-cted/
├── backend/
│   ├── SesiCursos.Api/          # Web API em C# (.NET 8)
│   │   ├── Controllers/         # CoursesController e HealthController
│   │   ├── Data/                # AppDbContext e DbSeeder (dados iniciais)
│   │   ├── DTOs/                # Objetos de retorno da API
│   │   ├── Models/              # Classes das tabelas (Course, Module, Lesson, Faq)
│   │   └── Program.cs           # Configuração da API, banco e arquivos estáticos
│   └── SesiCursos.Tests/        # Testes automatizados com xUnit
├── frontend/
│   ├── assets/                  # Fontes, ícones SVG e imagens
│   ├── css/style.css            # Estilos das páginas e responsividade
│   ├── js/                      # Scripts de cada tela (api, main, catalogo, detalhes, ava)
│   ├── index.html               # Página inicial
│   ├── cursos.html              # Página de catálogo e filtros
│   ├── detalhes.html            # Página de detalhes do curso
│   └── ava.html                 # Página do AVA
├── scripts/
│   ├── init_database_postgres.sql
│   ├── init_database_sqlite.sql
│   └── hybrid_server.py         # Script auxiliar para rodar com Python + SQLite
├── iniciar.sh / iniciar.bat     # Atalhos para subir o projeto no Mac ou Windows
├── testar.sh / testar.bat       # Atalhos para rodar os testes
├── README.md
└── USO_DE_IA.md
```

## 5. Como Rodar o Projeto (Windows e Mac)

Deixei o projeto preparado para rodar tanto no **Windows** quanto no **Mac**, pelo terminal ou pelo VS Code.

### Pré-requisitos
- Ter o **.NET 8 SDK** instalado (ou **Python 3**, caso queira rodar pelo script auxiliar que lê o mesmo banco SQLite).

### Passo a passo pelo Terminal

**No Mac / Linux:**
```bash
./iniciar.sh
```

**No Windows (CMD ou PowerShell):**
```powershell
.\iniciar.bat
```

**Ou rodando direto com o comando do .NET:**
```bash
cd backend/SesiCursos.Api
dotnet run
```

Depois que o servidor iniciar, basta abrir no navegador:
- **Site (Front-end):** `http://localhost:5298`
- **Catálogo de Cursos:** `http://localhost:5298/cursos.html`
- **Swagger da API:** `http://localhost:5298/swagger`

### Rodando os Testes

Para rodar os 11 testes que verificam a listagem, busca sem acento, filtros e erro 404:
```bash
# Mac / Linux
./testar.sh

# Windows
.\testar.bat

# Ou direto pelo .NET
dotnet test backend/SesiCursos.Tests
```

## 6. Banco de Dados

- **Local (SQLite):** Quando a aplicação sobe pela primeira vez, o `DbSeeder.cs` verifica se a tabela `Courses` está vazia e já cadastra os 8 cursos com seus módulos, aulas e dúvidas frequentes no arquivo `sesicursos.db`.
- **PostgreSQL:** Se quiser conectar em um banco PostgreSQL, basta informar a conexão na variável de ambiente `DATABASE_URL` ou no `appsettings.json`. O script `scripts/init_database_postgres.sql` também possui os comandos `CREATE TABLE` e `INSERT` prontos.

## 7. Rotas da API (Endpoints)

### `GET /api/courses`
Retorna a lista de cursos e aceita os seguintes parâmetros opcionais na URL:
- `search`: busca por parte do nome ou descrição (ex: `/api/courses?search=metalurgia`)
- `category`: filtra pela categoria (ex: `/api/courses?category=Matemática`)
- `modality`: filtra pela modalidade (ex: `/api/courses?modality=Online`)
- `status`: filtra pelo status (ex: `/api/courses?status=Inscrições Abertas`)

### `GET /api/courses/{id}`
Retorna todos os dados de um curso específico pelo `id`, incluindo a lista de módulos, aulas e perguntas frequentes. Se o `id` não existir, retorna `404 Not Found`.

### `GET /api/health`
Rota simples para testar se a API e a conexão com o banco estão funcionando (`200 OK`).

## 8. Modelo de Dados

O banco foi dividido em 4 tabelas principais:
- **Courses**: dados gerais do curso (`Id`, `FullName`, `ShortName`, `Description`, `Category`, `Modality`, `WorkloadHours`, `TargetAudience`, `Status`, `ImageUrl`, `AvailableDate`, `Certification`).
- **Modules**: módulos de cada curso (`Id`, `CourseId`, `Title`, `Order`).
- **Lessons**: aulas dentro de cada módulo (`Id`, `ModuleId`, `Title`, `Description`, `DurationMinutes`, `Order`).
- **Faqs**: perguntas e respostas do curso (`Id`, `CourseId`, `Question`, `Answer`, `Order`).

## 9. Responsividade e Acessibilidade

- **Responsividade:** Usei `grid` e `flexbox` com `@media` queries no `style.css` (`1024px`, `768px` e `480px`) para ajustar as colunas, os cards, o menu e a barra lateral do AVA em telas de tablet e celular.
- **Acessibilidade:**
  - Link no topo para pular direto para o conteúdo principal (`skip-link`).
  - Uso de tags semânticas (`header`, `nav`, `main`, `section`, `article`, `aside`, `footer`).
  - Contorno de foco visível (`:focus-visible`) para quem navega com `Tab`.
  - Cards e carrosséis funcionando pelo teclado (`Enter`, `Espaço` e setas).
  - Atributos `aria-label`, `aria-expanded` nos accordions e `aria-live="polite"` para avisar mudanças na busca.

## 10. Diferenças em relação ao Figma

Segui o visual do protótipo do Figma à risca nas três telas desenhadas no arquivo original:
- **Página Inicial (`index.html` - Flow 01)**: mantive as mesmas cores, fontes, imagens, textos, carrossel de cursos, depoimentos, rodapé e o efeito do menu superior que começa transparente/vermelho e fica branco fixo ao rolar a página.
- **Detalhes do Curso (`detalhes.html` - Flow 02)**: mantive o mesmo layout, ícones, textos de "Sobre o curso", os 3 accordions ("Módulos", "Aulas online e avaliações" e "Dúvidas frequentes") com os mesmos textos do protótipo e a barra inferior de matrícula que aparece ao rolar.
- **Ambiente Virtual de Aprendizagem (`ava.html` - Flow 03)**: segui exatamente a barra lateral dividida (topo escuro com progresso em `10%` e parte de baixo branca com a busca e a lista de módulos/aulas) e o conteúdo de "Boas práticas" e "Recomendações".

**O que foi adicionado além das telas do Figma e o motivo:**
- **Tela de Catálogo com Busca e Filtros (`cursos.html`)**: no protótipo do Figma não havia uma tela separada desenhada especificamente com os campos de busca por nome, filtros de Categoria/Modalidade/Status, contador de resultados e botão de limpar filtros, mas esses itens eram obrigatórios nas instruções do edital. Por isso, criei a página `cursos.html` mantendo exatamente o mesmo cabeçalho, os mesmos cards de curso, a mesma paleta de cores (`#A60502`, `#0C0C0D`, `#FAF9F6`), a fonte Open Sans e o mesmo rodapé das outras páginas para não fugir da identidade visual do Figma.

## 11. Como Publicar Gratuitamente (Deploy)

Para publicar o projeto gratuitamente no **Render** ou **Railway**:
1. Subir o código em um repositório no GitHub.
2. Criar um Web Service apontando para o repositório:
   - **Build Command:** `dotnet publish backend/SesiCursos.Api/SesiCursos.Api.csproj -c Release -o out`
   - **Start Command:** `dotnet out/SesiCursos.Api.dll`
3. Como a API já serve a pasta `frontend/`, o front-end e o back-end ficam disponíveis juntos no mesmo link gerado pela plataforma.

## 12. Limitações do Escopo

Seguindo as orientações do edital, o projeto foca na consulta, busca e visualização dos cursos, portanto não possui tela de login, área administrativa de cadastro/edição (CRUD) ou integração com gateway de pagamento.
