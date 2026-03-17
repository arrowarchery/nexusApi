using FastEndpoints;
using NexusAPI.DTO.EventRecurrence.Request;
using NexusAPI.DTO.EventRecurrence.Response;

namespace NexusAPI.Endpoints.EventRecurrence;

public class UpdateEventRecurrenceEndpoint(NexusDbContext nexusDbContext) : Endpoint<UpdateEventRecurrenceDto, GetEventRecurrenceDto>
{
    public override void Configure()
    {
        Put("/eventrecurrences");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateEventRecurrenceDto req, CancellationToken ct)
    {
        // On récupère l'existant pour être sûr de ne pas perdre de données
        var eventRecurrence = await nexusDbContext.EventRecurrence.FindAsync(new object[] { req.Id }, ct);
    
        if (eventRecurrence == null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        // Mise à jour des valeurs
        eventRecurrence.Type = req.Type;
        eventRecurrence.Title = req.Title;
        eventRecurrence.Frequency = req.Frequency;
        eventRecurrence.DateStart = req.DateStart;
        eventRecurrence.DateEnd = req.DateEnd;
        eventRecurrence.StartTime = req.StartTime;
        eventRecurrence.EndTime = req.EndTime;
        eventRecurrence.Day = req.Day;
        eventRecurrence.ClassId = req.ClassId;
        eventRecurrence.SportId = req.SportId;
        eventRecurrence.ExtraActivityId = req.ExtraActivityId;

        await nexusDbContext.SaveChangesAsync(ct);

        GetEventRecurrenceDto response = new()
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

        await Send.OkAsync(response, ct);
    }
}