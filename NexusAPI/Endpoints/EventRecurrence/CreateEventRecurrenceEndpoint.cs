using FastEndpoints;
using NexusAPI.DTO.EventRecurrence.Request;
using NexusAPI.DTO.EventRecurrence.Response;
using IMapper = AutoMapper.IMapper;

namespace NexusAPI.Endpoints.EventRecurrence;

public class CreateEventRecurrenceEndpoint(NexusDbContext db, IMapper mapper)
    : Endpoint<CreateEventRecurrenceDto, GetEventRecurrenceDto>
{
    public override void Configure()
    {
        Post("/eventrecurrences");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateEventRecurrenceDto req, CancellationToken ct)
    {
        // On convertit le DTO en entité
        var eventRecurrence = mapper.Map<Models.EventRecurrence>(req);

        db.EventRecurrence.Add(eventRecurrence);
        await db.SaveChangesAsync(ct);

        // On renvoie le résultat mappé en DTO (avec l'ID généré)
        await Send.OkAsync(mapper.Map<GetEventRecurrenceDto>(eventRecurrence), ct);
    }
}