namespace NexusAPI.DTO.Session.Request;

public class UpdateSessionDto
{
    public int Id { get; set; } 
    
    public DateTime? DateTimeStart { get; set; }
    public DateTime? DateTimeEnd { get; set; }
    public string? Status { get; set; }
    
    public List<int>? ActivityIds { get; set; }
    public List<int>? AchievementIds { get; set; }
}