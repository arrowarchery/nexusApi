using NexusAPI.DTO.Session.Response;
using NexusAPI.DTO.Achievement.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using NexusAPI.Models;

namespace NexusAPI.Endpoints.Session;

public class GetSessionRequest
{
    public int Id { get; set; }
}

public class GetSessionEndpoint(NexusDbContext db)
    : Endpoint<GetSessionRequest, GetSessionDto>
{
    public override void Configure()
    {
        Get("/sessions/{@id}", x => new { x.Id });
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetSessionRequest req, CancellationToken ct)
    {
        // Charger la session avec ses achievements
        var session = await db.Sessions
            .Include(s => s.SessionAchievements)       // charger la table de liaison
            .ThenInclude(sa => sa.Achievement)     // charger les achievements
            .SingleOrDefaultAsync(s => s.Id == req.Id, ct);

        if (session == null)
        {
            Console.WriteLine($"Aucune session avec l'ID {req.Id} trouvée.");
            await Send.NotFoundAsync(ct);
            return;
        }

        // Préparer la liste des achievements pour le DTO
        var achievements = session.SessionAchievements
            .Select(sa => new GetAchievementDto
            {
                Id = sa.Achievement.Id,
                Name = sa.Achievement.Name,
                Description = sa.Achievement.Description
            })
            .ToList();

        // Construire le DTO de réponse
        var responseDto = new GetSessionDto
        {
            Id = session.Id,
            DateTimeStart = session.DateTimeStart,
            DateTimeEnd = session.DateTimeEnd,
            Status = session.Status,
            Achievements = achievements,
            ClassId = session.ClassId,
            SportId = session.SportId,
            ExtraActivityId = session.ExtraActivityId,
        };

        await Send.OkAsync(responseDto, ct);
    }
}