namespace NexusAPI.DTO.SmartReminder.Response;

public class GetSmartReminderDto
{
    public int Id { get; set; }
    public DateOnly? DateAlert { get; set; }
    public TimeOnly? TimeAlert { get; set; }
    public string? Type { get; set; }
    public string? Status { get; set; }
}