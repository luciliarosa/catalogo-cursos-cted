# Uso de IA no Projeto (USO_DE_IA.md)

Este arquivo descreve como utilizei ferramentas de IA durante o desenvolvimento do teste prático para a vaga de **Desenvolvedor de Sistemas I (SESI-SP / CTED)**.

## 1. Qual ferramenta utilizei

Usei o **ChatGPT** **Gemini** **Antigravity** **Claude IA** como apoio durante o desenvolvimento para tirar dúvidas de código em C# (.NET 8), JavaScript e CSS.

## 2. Como e por que utilizei

Como optei por fazer o front-end com HTML, CSS e JavaScript puro (sem usar frameworks) e o back-end em C# com Entity Framework, usei o ChatGPT principalmente quando travava em alguma sintaxe específica ou queria lembrar a forma mais simples de resolver um problema sem precisar instalar bibliotecas extras.

## 3. Alguns prompts que usei

Durante o projeto, fiz perguntas diretas conforme as dúvidas iam surgindo:

- **Montar os filtros na URL (JavaScript):**
  *"Como passo parâmetros opcionais na URL usando fetch no JavaScript sem ficar concatenando vários ifs na string?"*

- **Ignorar acentos na busca (C#):**
  *"Como faço no C# para comparar duas strings ignorando acento, por exemplo se o usuário digitar textil achar Têxtil?"*

- **Rolagem lateral dos cards (JavaScript / CSS):**
  *"Como fazer uma div com vários cards rolar pro lado suavemente quando clico nos botões de seta usando JS puro?"*

- **Dúvidas de acessibilidade (HTML):**
  *"Quais atributos aria devo colocar num botão de accordion que abre e fecha para funcionar bem com leitor de tela?"*

## 4. O que aproveitei e o que descartei

**O que aproveitei:**
- A dica de usar `URLSearchParams` no JavaScript para montar a query string dos filtros (`/api/courses?search=...&category=...`), que deixou o código bem mais limpo.
- A função em C# com `NormalizationForm.FormD` para remover os acentos antes de filtrar os cursos pelo nome.
- O uso do método `scrollBy({ left: 310, behavior: 'smooth' })` para fazer o carrossel de cursos e o de depoimentos funcionarem de forma simples.
- Atributos de acessibilidade como `aria-expanded` nos botões de sanfona, `aria-live="polite"` na contagem de resultados e o link de pular para o conteúdo principal (`skip-link`).

**O que descartei:**
- Em algumas respostas a IA sugeriu usar Bootstrap, Tailwind ou criar o projeto em React. Preferi descartar e fazer tudo em HTML, CSS e JS puro para manter o projeto simples, leve e fiel ao que estudei.
- Também descartei sugestões de usar bibliotecas externas para requisição (como Axios) ou paginação complexa que não tinha no layout do Figma.

## 5. Como testei e validei o código

Nada foi colocado no projeto sem testar antes:
1. Subi a API em C# na porta `5298` e testei as rotas `/api/courses` e `/api/courses/{id}` pelo Swagger e pelo navegador para ver se o JSON estava vindo certo.
2. Abri todas as telas no navegador (`index.html`, `cursos.html`, `detalhes.html` e `ava.html`) e testei os fluxos: pesquisar palavras com e sem acento, combinar filtros, limpar filtros, abrir os módulos na página de detalhes e navegar até o AVA.
3. Redimensionei a tela pelo DevTools do navegador para checar se não estava quebrando no celular e testei a navegação usando a tecla `Tab`.
4. Criei e rodei os testes no projeto `SesiCursos.Tests` (`dotnet test`) para garantir que os filtros e o retorno de erro 404 continuassem funcionando.

## 6. Exemplo de erro identificado e correção realizada

- **Problema encontrado:**  
  No começo, a busca por nome sugerida pela IA usava apenas `.ToLower().Contains()`. Quando eu testava digitando `"textil"` ou `"precisao"` (sem acento), a API não retornava os cursos correspondentes porque no banco eles estavam cadastrados com acento (`"Têxtil"` e `"Precisão"`). Além disso, outra sugestão inicial usava uma função específica do PostgreSQL (`unaccent`), que causava erro de execução ao rodar o projeto localmente com o SQLite.

- **Como corrigi:**  
  Tirei a dependência de funções exclusivas do banco e criei uma função auxiliar `NormalizeText` direto no `CoursesController.cs` usando `NormalizationForm.FormD`. Ela decompõe os caracteres acentuados e compara apenas o texto puro na memória. Assim, a busca passou a funcionar perfeitamente tanto no SQLite local quanto no PostgreSQL da nuvem.