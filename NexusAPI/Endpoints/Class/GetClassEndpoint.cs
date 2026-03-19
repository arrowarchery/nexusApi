using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using NexusAPI.DTO.Class.Response;
using IMapper = AutoMapper.IMapper;

namespace NexusAPI.Endpoints.Class;

public class GetClassRequest
{
    public int Id { get; set; }
}

public class GetClassEndpoint(NexusDbContext db, IMapper mapper) : Endpoint<GetClassRequest, GetClassDto>
{
    public override void Configure()
    {
        Get("/class/{Id}"); // Utilisation simplifiée de la route
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetClassRequest req, CancellationToken ct)
    {
        var @class = await db.Classes
            .SingleOrDefaultAsync(a => a.Id == req.Id, ct);

        if (@class == null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        await Send.OkAsync(mapper.Map<GetClassDto>(@class), ct);
    }
}