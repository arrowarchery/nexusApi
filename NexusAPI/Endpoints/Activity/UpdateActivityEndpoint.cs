using FastEndpoints;
using NexusAPI.DTO.Activity.Request;
using NexusAPI.DTO.Activity.Response;
using IMapper = AutoMapper.IMapper;

namespace NexusAPI.Endpoints.Activity;

public class UpdateActivityEndpoint(NexusDbContext db, IMapper mapper) 
    : Endpoint<UpdateActivityDto, GetActivityDto>
{
    public override void Configure()
    {
        Put("/api/activity/{Id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateActivityDto req, CancellationToken ct)
    {
        var activity = await db.Activities.FindAsync(new object[] { req.Id }, ct);

        if (activity == null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        // On écrase les propriétés de l'entité avec celles du DTO
        mapper.Map(req, activity);
        
        await db.SaveChangesAsync(ct);

        await Send.OkAsync(mapper.Map<GetActivityDto>(activity), ct);
    }
}