using FastEndpoints;
using NexusAPI.DTO.ExtraActivity.Request;
using NexusAPI.DTO.ExtraActivity.Response;
using IMapper = AutoMapper.IMapper;

namespace NexusAPI.Endpoints.ExtraActivity;

public class CreateExtraActivityEndpoint(NexusDbContext db, IMapper mapper) 
    : Endpoint<CreateExtraActivityDto, GetExtraActivityDto>
{
    public override void Configure()
    {
        Post("/extraactivity");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateExtraActivityDto req, CancellationToken ct)
    {
        // Mapping DTO -> Entité
        var extraactivity = mapper.Map<Models.ExtraActivity>(req);
        
        db.ExtraActivities.Add(extraactivity);
        await db.SaveChangesAsync(ct);
        
        // Mapping Entité -> DTO de réponse
        await Send.OkAsync(mapper.Map<GetExtraActivityDto>(extraactivity), ct);
    }
}