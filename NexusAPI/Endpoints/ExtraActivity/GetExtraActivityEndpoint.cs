using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using IMapper = AutoMapper.IMapper;
using NexusAPI.DTO.ExtraActivity.Response;

namespace NexusAPI.Endpoints.ExtraActivity;

public class GetExtraActivityEndpoint(NexusDbContext db, IMapper mapper) 
    : Endpoint<GetExtraActivityDto>
{
    public override void Configure()
    {
        Get("/extraactivity/{Id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetExtraActivityDto req, CancellationToken ct)
    {
        var extraActivity = await db.ExtraActivities
            .SingleOrDefaultAsync(a => a.Id == req.Id, ct);

        if (extraActivity == null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        await Send.OkAsync(mapper.Map<GetExtraActivityDto>(extraActivity), ct);
    }
}