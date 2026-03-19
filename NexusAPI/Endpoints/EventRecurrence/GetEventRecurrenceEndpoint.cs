using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using NexusAPI.DTO.EventRecurrence.Response;
using IMapper = AutoMapper.IMapper;

namespace NexusAPI.Endpoints.EventRecurrence;

public class GetEventRecurrenceRequest
{
    public int Id { get; set; }
}

public class GetEventRecurrenceEndpoint(NexusDbContext db, IMapper mapper)
    : Endpoint<GetEventRecurrenceRequest, GetEventRecurrenceDto>
{
    public override void Configure()
    {
        Get("/eventrecurrences/{Id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetEventRecurrenceRequest req, CancellationToken ct)
    {
        var eventRecurrence = await db.EventRecurrence
            .SingleOrDefaultAsync(e => e.Id == req.Id, ct);

        if (eventRecurrence == null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        await Send.OkAsync(mapper.Map<GetEventRecurrenceDto>(eventRecurrence), ct);
    }
}