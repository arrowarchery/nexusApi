using NexusAPI.DTO.Achievement.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using IMapper = AutoMapper.IMapper;

namespace NexusAPI.Endpoints.Achievement;

public class GetAchievementRequest
{
    public int Id { get; set; }
}

public class GetAchievementEndpoint(NexusDbContext db, IMapper mapper)
    : Endpoint<GetAchievementRequest, GetAchievementDto>
{
    public override void Configure()
    {
        Get("/achievements/{Id}"); // Utilisation de la syntaxe standard
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetAchievementRequest req, CancellationToken ct)
    {
        var achievement = await db.Achievements
            .Include(a => a.SessionAchievements)
            .ThenInclude(sa => sa.Session)
            .SingleOrDefaultAsync(a => a.Id == req.Id, ct);

        if (achievement == null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        var responseDto = mapper.Map<GetAchievementDto>(achievement);

        await Send.OkAsync(responseDto, ct);
    }
}