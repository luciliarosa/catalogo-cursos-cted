# Etapa 1: Build da API em .NET 8
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia o arquivo do projeto e restaura as dependências
COPY ["backend/SesiCursos.Api/SesiCursos.Api.csproj", "backend/SesiCursos.Api/"]
RUN dotnet restore "backend/SesiCursos.Api/SesiCursos.Api.csproj"

# Copia o restante dos arquivos do backend e gera o build de produção
COPY backend/SesiCursos.Api/ backend/SesiCursos.Api/
WORKDIR "/src/backend/SesiCursos.Api"
RUN dotnet publish "SesiCursos.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Etapa 2: Imagem final leve para rodar no Render
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copia os arquivos publicados da API e a pasta do frontend estático
COPY --from=build /app/publish .
COPY frontend/ ./frontend/

# Porta padrão usada pelo Render
ENV PORT=10000
ENV ASPNETCORE_URLS=http://0.0.0.0:10000
EXPOSE 10000

ENTRYPOINT ["dotnet", "SesiCursos.Api.dll"]
