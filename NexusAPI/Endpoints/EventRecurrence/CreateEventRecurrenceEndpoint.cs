using FastEndpoints;
using NexusAPI.DTO.EventRecurrence.Request;
using NexusAPI.DTO.EventRecurrence.Response;

namespace NexusAPI.Endpoints.EventRecurrence;

public class CreateEventRecurrenceEndpoint(NexusDbContext db)
    : Endpoint<CreateEventRecurrenceDto, GetEventRecurrenceDto>
{
    public override void Configure()
    {
        Post("/eventrecurrences");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateEventRecurrenceDto req, CancellationToken ct)
    {
        var eventRecurrence = new Models.EventRecurrence
        {
            Type = req.Type,
            Title = req.Title,
            Frequency = req.Frequency,
            DateStart = req.DateStart,
            DateEnd = req.DateEnd,
            StartTime = req.StartTime,
            EndTime = req.EndTime,
            Day = req.Day,
            // Ajout des liens d'activité
            ClassId = req.ClassId,
            SportId = req.SportId,
            ExtraActivityId = req.ExtraActivityId
        };

        db.EventRecurrence.Add(eventRecurrence);
        await db.SaveChangesAsync(ct);

        var responseDto = new GetEventRecurrenceDto
        {
            Id = eventRecurrence.Id,
            Type = eventRecurrence.Type,
            Title = eventRecurrence.Title,
            Frequency = eventRecurrence.Frequency,
            DateStart = eventRecurrence.DateStart,
            DateEnd = eventRecurrence.DateEnd,
            StartTime = eventRecurrence.StartTime,
            EndTime = eventRecurrence.EndTime,
            Day = eventRecurrence.Day,
            ClassId = eventRecurrence.ClassId,
            SportId = eventRecurrence.SportId,
            ExtraActivityId = eventRecurrence.ExtraActivityId
        };

        await Send.OkAsync(responseDto, ct);
    }
}