namespace NexusAPI.Models;

public class Session
{
    public int Id { get; set; }
    public DateTime? DateTimeStart { get; set; }
    public DateTime? DateTimeEnd { get; set; }
    public string? Status { get; set; }
    
    public int LoginId { get; set; }

    public ICollection<Activity> Activities { get; set; } = new List<Activity>();

    public ICollection<SessionAchievement> SessionAchievements { get; set; } = new List<SessionAchievement>();
}