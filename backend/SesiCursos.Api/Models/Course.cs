namespace SesiCursos.Api.Models;

public class Course
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

    public List<CourseModule> Modules { get; set; } = new();
    public List<CourseFaq> Faqs { get; set; } = new();
}
