using System.Text.Json.Serialization;

namespace SesiCursos.Api.Models;

public class CourseFaq
{
    public int Id { get; set; }
    public int CourseId { get; set; }

    [JsonIgnore]
    public Course? Course { get; set; }

    public string Question { get; set; } = string.Empty;
    public string Answer { get; set; } = string.Empty;
    public int Order { get; set; }
}
