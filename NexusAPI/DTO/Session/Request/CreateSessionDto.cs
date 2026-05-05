using System.ComponentModel.DataAnnotations;

namespace NexusAPI.DTO.Session.Request;

public class CreateSessionDto
{
    [Required]
    public DateTime DateTimeStart { get; set; }
    
    public DateTime? DateTimeEnd { get; set; }
    
    public string? Status { get; set; }
    
    [Required]
    public int LoginId { get; set; } 

    public List<int>? ActivityIds { get; set; }
    
    public List<int>? AchievementIds { get; set; }
}