using NexusAPI.Models;

namespace NexusAPI.DTO.EventRecurrence.Request;

public class CreateEventRecurrenceDto
{
    public string? RecurrenceType { get; set; }
    public int Frequency { get; set; }
    public string? Title { get; set; }
    public DateOnly? DateStart { get; set; }
    public DateOnly? DateEnd { get; set; }
    public DayOfWeek? Day { get; set; }
}