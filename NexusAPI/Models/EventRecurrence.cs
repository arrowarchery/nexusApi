using System.ComponentModel.DataAnnotations;
using NexusAPI.Models.Enums;

namespace NexusAPI.Models;

public class EventRecurrence
{
    [Key] public int Id { get; set; }
    public string? Title { get; set; }
     public RecurrenceType Type { get; set; }
     public int Frequency { get; set; }
     public DateOnly? DateStart { get; set; }
     public DateOnly? DateEnd { get; set; }
    
     public TimeOnly StartTime { get; set; }
     public TimeOnly EndTime { get; set; }

    public DayOfWeek? Day { get; set; }
    public List<Session>? Sessions { get; set; }
}