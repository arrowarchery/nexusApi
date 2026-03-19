using NexusAPI.DTO.Session.Request;
using NexusAPI.DTO.Session.Response;
using IMapper = AutoMapper.IMapper;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using NexusAPI.Models;

namespace NexusAPI.Endpoints.Session;

public class CreateSessionEndpoint(NexusDbContext db, IMapper mapper)
    : Endpoint<CreateSessionDto, GetSessionDto>
{
    public override void Configure()
    {
        Post("/sessions");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateSessionDto req, CancellationToken ct)
    {
        var session = mapper.Map<Models.Session>(req);

        db.Sessions.Add(session);
        await db.SaveChangesAsync(ct);

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

        // On recharge avec les inclusions pour que le Mapper puisse trouver le "ActivityName" par aplatissement
        var sessionWithDetails = await db.Sessions
            .Include(s => s.Class)
            .Include(s => s.Sport)
            .Include(s => s.ExtraActivity)
            .FirstAsync(s => s.Id == session.Id, ct);

        await Send.OkAsync(mapper.Map<GetSessionDto>(sessionWithDetails), ct);
    }
}