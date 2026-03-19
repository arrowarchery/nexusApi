using NexusAPI.DTO.Session.Request;
using NexusAPI.DTO.Session.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using IMapper = AutoMapper.IMapper;

namespace NexusAPI.Endpoints.Session;

public class UpdateSessionEndpoint(NexusDbContext db, IMapper mapper)
    : Endpoint<UpdateSessionDto, GetSessionDto>
{
    public override void Configure()
    {
        Put("/sessions/{Id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateSessionDto req, CancellationToken ct)
    {
        var sessionToEdit = await db.Sessions
            .Include(s => s.SessionAchievements)
            .ThenInclude(sa => sa.Achievement)
            .SingleOrDefaultAsync(x => x.Id == req.Id, ct);

        if (sessionToEdit == null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        // Mise à jour automatique des champs simples
        mapper.Map(req, sessionToEdit);

        await db.SaveChangesAsync(ct);

        // On recharge pour avoir les noms d'activité à jour dans la réponse
        var result = await db.Sessions
            .Include(s => s.Class)
            .Include(s => s.Sport)
            .Include(s => s.ExtraActivity)
            .FirstAsync(s => s.Id == sessionToEdit.Id, ct);

        await Send.OkAsync(mapper.Map<GetSessionDto>(result), ct);
    }
}