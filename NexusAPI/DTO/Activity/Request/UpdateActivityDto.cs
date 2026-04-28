namespace NexusAPI.DTO.Activity.Request;

public class UpdateActivityDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Room { get; set; }
    public string? TypeLabel { get; set; }
}