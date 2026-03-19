using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using NexusAPI.DTO.Activity.Response;
using IMapper = AutoMapper.IMapper;

namespace NexusAPI.Endpoints.Activity;

public class GetActivityEndpoint(NexusDbContext db, IMapper mapper) : Endpoint<GetActivityDto>
{
    public override void Configure()
    {
        Get("/activity/{Id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetActivityDto req, CancellationToken ct)
    {
        var activity = await db.Activities
            .SingleOrDefaultAsync(a => a.Id == req.Id, ct);

        if (activity == null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        // Le mapper s'occupe de tout
        await Send.OkAsync(mapper.Map<GetActivityDto>(activity), ct);
    }
}