using NexusAPI.DTO.Achievement.Response;

namespace NexusAPI.DTO.Session.Response;

public class GetSessionDto {
    public int Id { get; set; }
    public DateTime? DateTimeStart { get; set; }
    public DateTime? DateTimeEnd { get; set; }
    public string? Status { get; set; }
    public string? ActivityName { get; set; }
    
    public int? ClassId { get; set; }
    public int? SportId { get; set; }
    public int? ExtraActivityId { get; set; }
    
    public List<GetAchievementDto> Achievements { get; set; }
}