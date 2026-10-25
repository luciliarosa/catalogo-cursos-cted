using System.Globalization;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SesiCursos.Api.Data;
using SesiCursos.Api.DTOs;
using SesiCursos.Api.Models;

namespace SesiCursos.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class CoursesController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly ILogger<CoursesController> _logger;

    public CoursesController(AppDbContext context, ILogger<CoursesController> logger)
    {
        _context = context;
        _logger = logger;
    }

    // Remove acentos e deixa tudo minúsculo para facilitar a busca
    private static string NormalizeText(string? text)
    {
        if (string.IsNullOrWhiteSpace(text)) return string.Empty;
        var normalized = text.Normalize(NormalizationForm.FormD);
        var sb = new StringBuilder();
        foreach (var c in normalized)
        {
            var uc = CharUnicodeInfo.GetUnicodeCategory(c);
            if (uc != UnicodeCategory.NonSpacingMark)
            {
                sb.Append(c);
            }
        }
        return sb.ToString().Normalize(NormalizationForm.FormC).ToLowerInvariant().Trim();
    }

    // Lista os cursos e aplica os filtros se forem passados na URL
    [HttpGet]
    [ProducesResponseType(typeof(CourseListResponseDto), StatusCodes.Status200OK)]
    public async Task<ActionResult<CourseListResponseDto>> GetCourses(
        [FromQuery] string? search,
        [FromQuery] string? category,
        [FromQuery] string? modality,
        [FromQuery] string? status)
    {
        _logger.LogInformation("Consultando cursos: search='{Search}', category='{Category}', modality='{Modality}', status='{Status}'",
            search, category, modality, status);

        var allCourses = await _context.Courses
            .Include(c => c.Modules)
                .ThenInclude(m => m.Lessons)
            .AsNoTracking()
            .ToListAsync();

        var total = allCourses.Count;
        IEnumerable<Course> query = allCourses;

        // Filtra pelo texto digitado na busca
        if (!string.IsNullOrWhiteSpace(search))
        {
            var normalizedTerm = NormalizeText(search);
            query = query.Where(c =>
                NormalizeText(c.FullName).Contains(normalizedTerm) ||
                NormalizeText(c.ShortName).Contains(normalizedTerm) ||
                NormalizeText(c.Description).Contains(normalizedTerm));
        }

        // Filtra por categoria
        if (!string.IsNullOrWhiteSpace(category))
        {
            var normalizedCat = NormalizeText(category);
            query = query.Where(c => NormalizeText(c.Category) == normalizedCat);
        }

        // Filtra por modalidade
        if (!string.IsNullOrWhiteSpace(modality))
        {
            var normalizedMod = NormalizeText(modality);
            query = query.Where(c => NormalizeText(c.Modality) == normalizedMod);
        }

        // Filtra por status
        if (!string.IsNullOrWhiteSpace(status))
        {
            var normalizedSt = NormalizeText(status);
            query = query.Where(c => NormalizeText(c.Status) == normalizedSt);
        }

        var filteredItems = query
            .OrderBy(c => c.Id)
            .Select(c => new CourseSummaryDto
            {
                Id = c.Id,
                FullName = c.FullName,
                ShortName = c.ShortName,
                Description = c.Description,
                Category = c.Category,
                Modality = c.Modality,
                WorkloadHours = c.WorkloadHours,
                TargetAudience = c.TargetAudience,
                Status = c.Status,
                ImageUrl = c.ImageUrl,
                AvailableDate = c.AvailableDate,
                Certification = c.Certification,
                ModulesCount = c.Modules.Count,
                LessonsCount = c.Modules.SelectMany(m => m.Lessons).Count()
            })
            .ToList();

        var categories = allCourses.Select(c => c.Category).Distinct().OrderBy(c => c).ToList();
        var modalities = allCourses.Select(c => c.Modality).Distinct().OrderBy(c => c).ToList();
        var statuses = allCourses.Select(c => c.Status).Distinct().OrderBy(c => c).ToList();

        var response = new CourseListResponseDto
        {
            Total = total,
            FilteredTotal = filteredItems.Count,
            Items = filteredItems,
            AvailableFilters = new FilterOptionsDto
            {
                Categories = categories,
                Modalities = modalities,
                Statuses = statuses
            }
        };

        return Ok(response);
    }

    // Busca os detalhes de um curso específico pelo ID
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(CourseDetailDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CourseDetailDto>> GetCourseById(int id)
    {
        _logger.LogInformation("Consultando detalhes do curso ID: {CourseId}", id);

        var course = await _context.Courses
            .Include(c => c.Modules.OrderBy(m => m.Order))
                .ThenInclude(m => m.Lessons.OrderBy(l => l.Order))
            .Include(c => c.Faqs.OrderBy(f => f.Order))
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);

        if (course == null)
        {
            _logger.LogWarning("Curso ID {CourseId} não encontrado.", id);
            return NotFound(new { message = $"Curso com ID {id} não encontrado." });
        }

        var dto = new CourseDetailDto
        {
            Id = course.Id,
            FullName = course.FullName,
            ShortName = course.ShortName,
            Description = course.Description,
            Category = course.Category,
            Modality = course.Modality,
            WorkloadHours = course.WorkloadHours,
            TargetAudience = course.TargetAudience,
            Status = course.Status,
            ImageUrl = course.ImageUrl,
            VideoUrl = course.VideoUrl,
            AvailableDate = course.AvailableDate,
            Certification = course.Certification,
            Modules = course.Modules.Select(m => new ModuleDto
            {
                Id = m.Id,
                Title = m.Title,
                Order = m.Order,
                Lessons = m.Lessons.Select(l => new LessonDto
                {
                    Id = l.Id,
                    Title = l.Title,
                    Description = l.Description,
                    DurationMinutes = l.DurationMinutes,
                    Order = l.Order
                }).ToList()
            }).ToList(),
            Faqs = course.Faqs.Select(f => new FaqDto
            {
                Id = f.Id,
                Question = f.Question,
                Answer = f.Answer,
                Order = f.Order
            }).ToList()
        };

        return Ok(dto);
    }
}
