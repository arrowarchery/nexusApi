using NexusAPI.DTO.Session.Response;

namespace NexusAPI.DTO.Achievement.Response;

public class GetAchievementDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    
    public List<GetSessionDto> Sessions { get; set; } = new();
}