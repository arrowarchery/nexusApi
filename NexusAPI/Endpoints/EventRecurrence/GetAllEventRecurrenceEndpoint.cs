using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using NexusAPI.DTO.EventRecurrence.Response;

namespace NexusAPI.Endpoints.EventRecurrence;

public class GetAllEventRecurrenceEndpoint(NexusDbContext nexusDbContext) : EndpointWithoutRequest<List<GetEventRecurrenceDto>>
{
    public override void Configure()
    {
        Get("/eventrecurrences");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        List<GetEventRecurrenceDto> responseDto = await nexusDbContext.EventRecurrence
            .Select(a => new GetEventRecurrenceDto()
            {
                Id = a.Id,
                Type = a.Type,
                Title = a.Title,
                Frequency = a.Frequency,
                DateStart = a.DateStart,
                DateEnd = a.DateEnd,
                StartTime = a.StartTime,
                EndTime = a.EndTime,
                Day = a.Day,
                ClassId = a.ClassId,
                SportId = a.SportId,
                ExtraActivityId = a.ExtraActivityId
            }).ToListAsync(ct);
        await Send.OkAsync(responseDto, ct);
    }
}