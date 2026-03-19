using NexusAPI.DTO.Session.Response;
using IMapper = AutoMapper.IMapper;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace NexusAPI.Endpoints.Session;

public class GetSessionRequest
{
    public int Id { get; set; }
}

public class GetSessionEndpoint(NexusDbContext db, IMapper mapper)
    : Endpoint<GetSessionRequest, GetSessionDto>
{
    public override void Configure()
    {
        Get("/sessions/{Id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetSessionRequest req, CancellationToken ct)
    {
        var session = await db.Sessions
            .Include(s => s.Class)
            .Include(s => s.Sport)
            .Include(s => s.ExtraActivity)
            .Include(s => s.SessionAchievements)
            .ThenInclude(sa => sa.Achievement)
            .SingleOrDefaultAsync(s => s.Id == req.Id, ct);

        if (session == null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        await Send.OkAsync(mapper.Map<GetSessionDto>(session), ct);
    }
}