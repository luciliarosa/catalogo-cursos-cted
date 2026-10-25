using System.Text.Json.Serialization;

namespace SesiCursos.Api.Models;

public class CourseLesson
{
    public int Id { get; set; }
    public int ModuleId { get; set; }

    [JsonIgnore]
    public CourseModule? Module { get; set; }

    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int DurationMinutes { get; set; }
    public int Order { get; set; }
}
