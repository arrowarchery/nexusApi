using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using NexusAPI.DTO.Session.Request;
using NexusAPI.DTO.Session.Response;
using NexusAPI.Models;
using IMapper = AutoMapper.IMapper;

namespace NexusAPI.Endpoints.Session;

public class CreateSessionEndpoint(NexusDbContext db, IMapper mapper)
    : Endpoint<CreateSessionDto, GetSessionDto>
{
    public override void Configure()
    {
        Post("/api/sessions"); // Cohérence avec le préfixe /api
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateSessionDto req, CancellationToken ct)
    {
        var session = mapper.Map<Models.Session>(req);

        if (req.ActivityIds != null && req.ActivityIds.Any())
        {
            var activities = await db.Activities
                .Where(a => req.ActivityIds.Contains(a.Id))
                .ToListAsync(ct);
            
            session.Activities = activities;
        }

        db.Sessions.Add(session);
        await db.SaveChangesAsync(ct);

        // Gestion des Achievements
        if (req.AchievementIds != null && req.AchievementIds.Any())
        {
            var sessionAchievements = req.AchievementIds.Select(id => new SessionAchievement 
            { 
                SessionId = session.Id, 
                AchievementId = id 
            });
            await db.SessionAchievements.AddRangeAsync(sessionAchievements, ct);
            await db.SaveChangesAsync(ct);
        }

        var sessionWithDetails = await db.Sessions
            .Include(s => s.Activities)
            .Include(s => s.SessionAchievements)
            .ThenInclude(sa => sa.Achievement)
            .FirstAsync(s => s.Id == session.Id, ct);

        await Send.OkAsync(mapper.Map<GetSessionDto>(sessionWithDetails), ct);
    }
}