namespace SesiCursos.Api.DTOs;

public class CourseListResponseDto
{
    public int Total { get; set; }
    public int FilteredTotal { get; set; }
    public List<CourseSummaryDto> Items { get; set; } = new();
    public FilterOptionsDto AvailableFilters { get; set; } = new();
}

public class FilterOptionsDto
{
    public List<string> Categories { get; set; } = new();
    public List<string> Modalities { get; set; } = new();
    public List<string> Statuses { get; set; } = new();
}
