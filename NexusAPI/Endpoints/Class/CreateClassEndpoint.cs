using FastEndpoints;
using NexusAPI.DTO.Class.Request;
using NexusAPI.DTO.Class.Response;
using IMapper = AutoMapper.IMapper;

namespace NexusAPI.Endpoints.Class;

public class CreateClassEndpoint(NexusDbContext db, IMapper mapper) : Endpoint<CreateClassDto, GetClassDto>
{
    public override void Configure()
    {
        Post("/class");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateClassDto req, CancellationToken ct)
    {
        // Mapping DTO -> Entity
        var @class = mapper.Map<Models.Class>(req);
        
        db.Activities.Add(@class);
        await db.SaveChangesAsync(ct);
        
        // Mapping Entity -> DTO de réponse
        var response = mapper.Map<GetClassDto>(@class);
        
        await Send.OkAsync(response, ct);
    }
}