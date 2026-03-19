using FastEndpoints;
using NexusAPI.DTO.Activity.Request;
using NexusAPI.DTO.Activity.Response;
using IMapper = AutoMapper.IMapper;

namespace NexusAPI.Endpoints.Activity;

public class CreateActivityEndpoint(NexusDbContext db, IMapper mapper) 
    : Endpoint<CreateActivityDto, GetActivityDto>
{
    public override void Configure()
    {
        Post("/activity");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateActivityDto req, CancellationToken ct)
    {
        // DTO -> Model
        var activity = mapper.Map<Models.Activity>(req);
        
        db.Activities.Add(activity);
        await db.SaveChangesAsync(ct);
        
        // Model -> DTO
        var response = mapper.Map<GetActivityDto>(activity);
        
        await Send.OkAsync(response, ct);
    }
}