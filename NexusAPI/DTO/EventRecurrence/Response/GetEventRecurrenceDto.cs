using NexusAPI.Models.Enums;

namespace NexusAPI.DTO.EventRecurrence.Response;

public class GetEventRecurrenceDto
{
    public int Id { get; set; }
    public RecurrenceType Type { get; set; }
    public int Frequency { get; set; }
    public string? Title { get; set; }
    public DateOnly? DateStart { get; set; }
    public DateOnly? DateEnd { get; set; }
    public TimeOnly? StartTime { get; set; }
    public TimeOnly? EndTime { get; set; }
    public DayOfWeek? Day { get; set; }

    // Ajouts pour renvoyer les IDs au Frontend
    public int? ClassId { get; set; }
    public int? SportId { get; set; }
    public int? ExtraActivityId { get; set; }
}