namespace SesiCursos.Api.DTOs;

public class CourseDetailDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string ShortName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Modality { get; set; } = string.Empty;
    public int WorkloadHours { get; set; }
    public string TargetAudience { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string VideoUrl { get; set; } = string.Empty;
    public DateTime? AvailableDate { get; set; }
    public string Certification { get; set; } = "SESI";
    public List<ModuleDto> Modules { get; set; } = new();
    public List<FaqDto> Faqs { get; set; } = new();
}

public class ModuleDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int Order { get; set; }
    public List<LessonDto> Lessons { get; set; } = new();
}

public class LessonDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int DurationMinutes { get; set; }
    public int Order { get; set; }
}

public class FaqDto
{
    public int Id { get; set; }
    public string Question { get; set; } = string.Empty;
    public string Answer { get; set; } = string.Empty;
    public int Order { get; set; }
}
