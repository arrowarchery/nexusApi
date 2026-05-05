using NexusAPI.DTO.Session.Response;
using IMapper = AutoMapper.IMapper;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace NexusAPI.Endpoints.Session;

public class GetAllSessionEndpoint(NexusDbContext db, IMapper mapper)
    : EndpointWithoutRequest<List<GetSessionDto>>
{
    public override void Configure()
    {
        Get("/sessions");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var sessions = await db.Sessions
            .Include(s => s.Activities) // Inclus toutes les classes, sports et extras hérités
            .Include(s => s.SessionAchievements)
            .ThenInclude(sa => sa.Achievement)
            .AsNoTracking()
            .ToListAsync(ct);

        await Send.OkAsync(mapper.Map<List<GetSessionDto>>(sessions), ct);
    }
}