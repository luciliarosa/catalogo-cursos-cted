using System.Text.Json.Serialization;

namespace SesiCursos.Api.Models;

public class CourseModule
{
    public int Id { get; set; }
    public int CourseId { get; set; }

    [JsonIgnore]
    public Course? Course { get; set; }

    public string Title { get; set; } = string.Empty;
    public int Order { get; set; }

    public List<CourseLesson> Lessons { get; set; } = new();
}
