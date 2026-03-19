using FastEndpoints;
using NexusAPI.DTO.EventRecurrence.Request;
using NexusAPI.DTO.EventRecurrence.Response;
using IMapper = AutoMapper.IMapper;

namespace NexusAPI.Endpoints.EventRecurrence;

public class UpdateEventRecurrenceEndpoint(NexusDbContext db, IMapper mapper) 
    : Endpoint<UpdateEventRecurrenceDto, GetEventRecurrenceDto>
{
    public override void Configure()
    {
        Put("/eventrecurrences/{Id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateEventRecurrenceDto req, CancellationToken ct)
    {
        var eventRecurrence = await db.EventRecurrence.FindAsync(new object[] { req.Id }, ct);
    
        if (eventRecurrence == null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        // Le mapper écrase les valeurs de l'entité existante avec celles du DTO
        mapper.Map(req, eventRecurrence);

        await db.SaveChangesAsync(ct);

        await Send.OkAsync(mapper.Map<GetEventRecurrenceDto>(eventRecurrence), ct);
    }
}