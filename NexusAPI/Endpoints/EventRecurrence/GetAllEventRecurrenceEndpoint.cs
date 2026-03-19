using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using NexusAPI.DTO.EventRecurrence.Response;
using IMapper = AutoMapper.IMapper;

namespace NexusAPI.Endpoints.EventRecurrence;

public class GetAllEventRecurrenceEndpoint(NexusDbContext db, IMapper mapper) 
    : EndpointWithoutRequest<List<GetEventRecurrenceDto>>
{
    public override void Configure()
    {
        Get("/eventrecurrences");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var recurrences = await db.EventRecurrence.AsNoTracking().ToListAsync(ct);
        
        // Mapping direct de la liste
        var responseDto = mapper.Map<List<GetEventRecurrenceDto>>(recurrences);
        
        await Send.OkAsync(responseDto, ct);
    }
}