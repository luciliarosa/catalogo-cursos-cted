using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SesiCursos.Api.Data;
using SesiCursos.Api.DTOs;
using Xunit;

namespace SesiCursos.Tests;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    private readonly string _dbPath = Path.Combine(Path.GetTempPath(), $"SesiTestDb_{Guid.NewGuid():N}.db");

    // Cria um banco SQLite temporário para rodar os testes
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));
            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlite($"Data Source={_dbPath}");
            });

            var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            DbSeeder.Seed(db);
        });
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        try
        {
            if (File.Exists(_dbPath))
            {
                File.Delete(_dbPath);
            }
        }
        catch { }
    }
}

public class CoursesApiTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public CoursesApiTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    // Testa se a listagem retorna todos os cursos quando não tem filtro
    [Fact]
    public async Task GetCourses_ReturnsAllCourses_WhenNoFiltersApplied()
    {
        var response = await _client.GetAsync("/api/courses");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<CourseListResponseDto>();
        result.Should().NotBeNull();
        result!.Total.Should().BeGreaterThanOrEqualTo(6);
        result.FilteredTotal.Should().Be(result.Total);
        result.Items.Should().HaveCount(result.Total);
        result.AvailableFilters.Categories.Should().HaveCountGreaterThanOrEqualTo(3);
        result.AvailableFilters.Modalities.Should().HaveCountGreaterThanOrEqualTo(2);
        result.AvailableFilters.Statuses.Should().HaveCountGreaterThanOrEqualTo(3);
    }

    // Testa busca parcial pelo nome
    [Fact]
    public async Task GetCourses_FiltersByName_CaseInsensitivePartialMatch()
    {
        var response = await _client.GetAsync("/api/courses?search=metalurgia");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<CourseListResponseDto>();
        result.Should().NotBeNull();
        result!.Items.Should().NotBeEmpty();
        result.Items.Should().OnlyContain(c => 
            c.FullName.ToLower().Contains("metalurgia") || 
            c.ShortName.ToLower().Contains("metalurgia") ||
            c.Description.ToLower().Contains("metalurgia"));
    }

    // Testa se buscar sem acento encontra o curso com acento
    [Fact]
    public async Task GetCourses_FiltersByName_AccentInsensitive()
    {
        var response = await _client.GetAsync("/api/courses?search=textil");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<CourseListResponseDto>();
        result.Should().NotBeNull();
        result!.Items.Should().NotBeEmpty();
        result.Items.Should().Contain(c => c.FullName.Contains("Têxtil"));
    }

    // Testa filtro por categoria
    [Fact]
    public async Task GetCourses_FiltersByCategory_ReturnsOnlyMatchingCategory()
    {
        var response = await _client.GetAsync("/api/courses?category=Matemática");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<CourseListResponseDto>();
        result.Should().NotBeNull();
        result!.Items.Should().NotBeEmpty();
        result.Items.Should().OnlyContain(c => c.Category.Equals("Matemática", StringComparison.OrdinalIgnoreCase));
    }

    // Testa filtro por modalidade
    [Fact]
    public async Task GetCourses_FiltersByModality_ReturnsOnlyMatchingModality()
    {
        var response = await _client.GetAsync("/api/courses?modality=Online");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<CourseListResponseDto>();
        result.Should().NotBeNull();
        result!.Items.Should().NotBeEmpty();
        result.Items.Should().OnlyContain(c => c.Modality.Equals("Online", StringComparison.OrdinalIgnoreCase));
    }

    // Testa filtro por status
    [Fact]
    public async Task GetCourses_FiltersByStatus_ReturnsOnlyMatchingStatus()
    {
        var response = await _client.GetAsync("/api/courses?status=Inscrições Abertas");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<CourseListResponseDto>();
        result.Should().NotBeNull();
        result!.Items.Should().NotBeEmpty();
        result.Items.Should().OnlyContain(c => c.Status.Equals("Inscrições Abertas", StringComparison.OrdinalIgnoreCase));
    }

    // Testa combinação de busca + filtros juntos
    [Fact]
    public async Task GetCourses_CombinesSearchAndFilters_Consistently()
    {
        var response = await _client.GetAsync("/api/courses?search=Precisão&category=Matemática&modality=Online");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<CourseListResponseDto>();
        result.Should().NotBeNull();
        result!.Items.Should().NotBeEmpty();
        result.Items.Should().OnlyContain(c => 
            c.Category == "Matemática" && 
            c.Modality == "Online" && 
            (c.FullName.Contains("Precisão") || c.Description.Contains("Precisão")));
    }

    // Testa quando a busca não encontra nenhum curso
    [Fact]
    public async Task GetCourses_ReturnsEmptyList_WhenNoMatches()
    {
        var response = await _client.GetAsync("/api/courses?search=termo_inexistente_123");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<CourseListResponseDto>();
        result.Should().NotBeNull();
        result!.FilteredTotal.Should().Be(0);
        result.Items.Should().BeEmpty();
    }

    // Testa busca de detalhes por ID válido
    [Fact]
    public async Task GetCourseById_ReturnsCourseDetails_WhenCourseExists()
    {
        var response = await _client.GetAsync("/api/courses/1");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<CourseDetailDto>();
        result.Should().NotBeNull();
        result!.Id.Should().Be(1);
        result.FullName.Should().Contain("Matemática para a Precisão");
        result.Modules.Should().NotBeEmpty();
        result.Modules.First().Lessons.Should().NotBeEmpty();
        result.Faqs.Should().NotBeEmpty();
    }

    // Testa retorno 404 quando o ID não existe
    [Fact]
    public async Task GetCourseById_ReturnsNotFound_WhenCourseDoesNotExist()
    {
        var response = await _client.GetAsync("/api/courses/99999");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    // Testa a rota de health check
    [Fact]
    public async Task HealthCheck_ReturnsHealthy()
    {
        var response = await _client.GetAsync("/api/health");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var content = await response.Content.ReadAsStringAsync();
        content.Should().Contain("Healthy");
    }
}
