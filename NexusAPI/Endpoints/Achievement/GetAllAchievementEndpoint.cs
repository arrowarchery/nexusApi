using NexusAPI.DTO.Achievement.Response;
using IMapper = AutoMapper.IMapper;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace NexusAPI.Endpoints.Achievement;

public class GetAllAchievementEndpoint(NexusDbContext db, IMapper mapper)
    : EndpointWithoutRequest<List<GetAchievementDto>>
{
    public override void Configure()
    {
        Get("/achievements");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var achievements = await db.Achievements
            .Include(a => a.SessionAchievements)
            .ThenInclude(sa => sa.Session)
            .AsNoTracking()
            .ToListAsync(ct);

        // Le mapper s'occupe de transformer toute la liste et les sessions imbriquées
        var responseDto = mapper.Map<List<GetAchievementDto>>(achievements);

        await Send.OkAsync(responseDto, ct);
    }
}