using NexusAPI.DTO.Achievement.Request;
using NexusAPI.DTO.Achievement.Response;
using IMapper = AutoMapper.IMapper;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using NexusAPI.Models;

namespace NexusAPI.Endpoints.Achievement;

public class UpdateAchievementEndpoint(NexusDbContext db, IMapper mapper)
    : Endpoint<UpdateAchievementDto, GetAchievementDto>
{
    public override void Configure()
    {
        Put("/achievements/{Id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateAchievementDto req, CancellationToken ct)
    {
        var achievementToEdit = await db.Achievements
            .Include(a => a.SessionAchievements)
            .SingleOrDefaultAsync(x => x.Id == req.Id, ct);

        if (achievementToEdit == null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        // Mise à jour des propriétés simples via le Mapper
        mapper.Map(req, achievementToEdit);

        // Gestion manuelle des relations SessionIds (Table de liaison)
        if (req.SessionIds != null)
        {
            db.SessionAchievements.RemoveRange(achievementToEdit.SessionAchievements);

            foreach (var sessionId in req.SessionIds)
            {
                db.SessionAchievements.Add(new SessionAchievement
                {
                    AchievementId = achievementToEdit.Id,
                    SessionId = sessionId
                });
            }
        }

        await db.SaveChangesAsync(ct);

        // Recharger avec les nouvelles sessions pour la réponse
        var updatedEntity = await db.Achievements
            .Include(a => a.SessionAchievements)
            .ThenInclude(sa => sa.Session)
            .FirstAsync(a => a.Id == achievementToEdit.Id, ct);

        await Send.OkAsync(mapper.Map<GetAchievementDto>(updatedEntity), ct);
    }
}