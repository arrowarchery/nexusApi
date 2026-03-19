namespace NexusAPI.DTO.Activity.Response;

public class GetActivityDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public string? Room { get; set; }
    public string? TypeLabel { get; set; } 
}