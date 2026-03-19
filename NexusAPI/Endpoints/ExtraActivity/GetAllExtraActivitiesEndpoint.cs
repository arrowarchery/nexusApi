using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using IMapper = AutoMapper.IMapper;
using NexusAPI.DTO.ExtraActivity.Response;

namespace NexusAPI.Endpoints.ExtraActivity;

public class GetAllExtraActivitiesEndpoint(NexusDbContext db, IMapper mapper) 
    : EndpointWithoutRequest<List<GetExtraActivityDto>>
{
    public override void Configure()
    {
        Get("/extraactivity");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var extraActivities = await db.ExtraActivities.AsNoTracking().ToListAsync(ct);

        // Mapping de la liste complète
        var responseDto = mapper.Map<List<GetExtraActivityDto>>(extraActivities);

        await Send.OkAsync(responseDto, ct);
    }
}