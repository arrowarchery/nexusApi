
namespace NexusAPI.DTO.EventRecurrence.Request;

public class UpdateEventRecurrenceDto
{
    public int Id { get; set; }
    public string? RecurrenceType { get; set; }
    public int Frequency { get; set; }
    public string? Title { get; set; }
    public DateOnly? DateStart { get; set; }
    public DateOnly? DateEnd { get; set; }
    public DayOfWeek? Day { get; set; }
}