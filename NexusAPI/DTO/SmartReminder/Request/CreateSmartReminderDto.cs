namespace NexusAPI.DTO.SmartReminder.Request;

public class CreateSmartReminderDto
{
    public DateOnly? DateAlert { get; set; }
    public TimeOnly? TimeAlert { get; set; }
    public string? Type { get; set; }
    public string? Status { get; set; }
}