using FastEndpoints;
using IMapper = AutoMapper.IMapper;
using NexusAPI.DTO.ExtraActivity.Request;
using NexusAPI.DTO.ExtraActivity.Response;

namespace NexusAPI.Endpoints.ExtraActivity;

public class UpdateExtraActivityEndpoint(NexusDbContext db, IMapper mapper) 
    : Endpoint<UpdateExtraActivityDto, GetExtraActivityDto>
{
    public override void Configure()
    {
        Put("/api/extraactivity/{Id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateExtraActivityDto req, CancellationToken ct)
    {
        // On charge l'existant
        var existingActivity = await db.ExtraActivities.FindAsync(new object[] { req.Id }, ct);
        
        if (existingActivity == null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        // Le mapper met à jour l'entité existante
        mapper.Map(req, existingActivity);
        
        await db.SaveChangesAsync(ct);

        await Send.OkAsync(mapper.Map<GetExtraActivityDto>(existingActivity), ct);
    }
}