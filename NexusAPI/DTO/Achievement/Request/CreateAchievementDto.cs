namespace NexusAPI.DTO.Achievement.Request;

public class CreateAchievementDto
{
    public string? Name { get; set; }
    public string? Description { get; set; }

    public List<int>? SessionIds { get; set; }
}