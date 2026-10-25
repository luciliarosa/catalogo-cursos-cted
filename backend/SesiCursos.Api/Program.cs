using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SesiCursos.Api.Data;

var builder = WebApplication.CreateBuilder(args);

// Configura os controllers e o retorno em JSON
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
    });

// Adiciona o Swagger para testar a API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "SESI Indústria - Catálogo de Cursos CTED API",
        Version = "v1",
        Description = "API de consulta para o catálogo de cursos do SESI-SP (CTED)",
        Contact = new OpenApiContact
        {
            Name = "SESI-SP - CTED",
            Url = new Uri("https://www.sesisp.org.br")
        }
    });
});

// Libera o CORS para o front-end conseguir chamar a API
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Configura a conexão com o banco (usa PostgreSQL se tiver configurado ou SQLite por padrão)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
                       ?? Environment.GetEnvironmentVariable("DATABASE_URL");

if (!string.IsNullOrWhiteSpace(connectionString) && 
    (connectionString.Contains("Host=") || connectionString.Contains("postgres://") || connectionString.Contains("postgresql://")))
{
    if (connectionString.StartsWith("postgres://") || connectionString.StartsWith("postgresql://"))
    {
        var uri = new Uri(connectionString);
        var userInfo = uri.UserInfo.Split(':');
        var user = userInfo[0];
        var password = userInfo.Length > 1 ? userInfo[1] : "";
        var port = uri.Port > 0 ? uri.Port : 5432;
        var database = uri.AbsolutePath.TrimStart('/');
        connectionString = $"Host={uri.Host};Port={port};Database={database};Username={user};Password={password};SSL Mode=Require;Trust Server Certificate=true";
    }

    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseNpgsql(connectionString));
    Console.WriteLine("[DB] Conectado ao PostgreSQL.");
}
else
{
    var dbFilePath = Path.Combine(builder.Environment.ContentRootPath, "sesicursos.db");
    var sqliteConn = connectionString;
    if (string.IsNullOrWhiteSpace(sqliteConn) || sqliteConn.Equals("Data Source=sesicursos.db", StringComparison.OrdinalIgnoreCase))
    {
        sqliteConn = $"Data Source={dbFilePath}";
    }
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlite(sqliteConn));
    Console.WriteLine($"[DB] Usando SQLite local ({sqliteConn}).");
}

// Usa a porta do Render (variável PORT) quando estiver na nuvem ou 5298 localmente
var renderPort = Environment.GetEnvironmentVariable("PORT");
if (!string.IsNullOrWhiteSpace(renderPort))
{
    builder.WebHost.UseUrls($"http://0.0.0.0:{renderPort}");
}
else if (string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("ASPNETCORE_URLS")) &&
         string.IsNullOrWhiteSpace(builder.Configuration["urls"]))
{
    builder.WebHost.UseUrls("http://localhost:5298");
}

var app = builder.Build();

// Cria o banco e insere os cursos iniciais caso ainda não existam
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    try
    {
        db.Database.EnsureCreated();
        DbSeeder.Seed(db);
        Console.WriteLine("[DB] Banco de dados pronto.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"[DB ERROR] Erro ao iniciar banco: {ex.Message}");
    }
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "SESI Catálogo API v1");
    c.RoutePrefix = "swagger";
});

app.UseCors("AllowAll");

// Serve os arquivos HTML, CSS e JS da pasta frontend
var possiblePaths = new[]
{
    Path.Combine(builder.Environment.ContentRootPath, "..", "..", "frontend"),
    Path.Combine(builder.Environment.ContentRootPath, "frontend"),
    Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "frontend"),
    Path.Combine(Directory.GetCurrentDirectory(), "frontend"),
    Path.Combine(AppContext.BaseDirectory, "frontend"),
    Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "..", "frontend")
};

string? validFrontendPath = possiblePaths.FirstOrDefault(p => Directory.Exists(p));
if (validFrontendPath != null)
{
    var resolvedPath = Path.GetFullPath(validFrontendPath);
    var fileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(resolvedPath);
    app.UseDefaultFiles(new DefaultFilesOptions
    {
        FileProvider = fileProvider
    });
    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = fileProvider
    });
    Console.WriteLine($"[WEB] Servindo pasta frontend: {resolvedPath}");
}

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }
