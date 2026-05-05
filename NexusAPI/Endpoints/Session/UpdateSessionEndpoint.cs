using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using NexusAPI.DTO.Session.Request;
using NexusAPI.DTO.Session.Response;
using IMapper = AutoMapper.IMapper;

namespace NexusAPI.Endpoints.Session;

public class UpdateSessionEndpoint(NexusDbContext db, IMapper mapper)
    : Endpoint<UpdateSessionDto, GetSessionDto>
{
    public override void Configure()
    {
        Put("/api/sessions/{Id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateSessionDto req, CancellationToken ct)
    {
        var sessionToEdit = await db.Sessions
            .Include(s => s.Activities) // On inclut la nouvelle liste
            .Include(s => s.SessionAchievements)
            .ThenInclude(sa => sa.Achievement)
            .SingleOrDefaultAsync(x => x.Id == req.Id, ct);

        if (sessionToEdit == null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        // Mise à jour des champs simples (DateTime, Status, etc.)
        mapper.Map(req, sessionToEdit);

        // Note : Si UpdateSessionDto contient une liste d'ActivityIds, 
        // vous devrez gérer ici la synchronisation de la collection sessionToEdit.Activities

        await db.SaveChangesAsync(ct);

        // On recharge avec Activities pour que le Mapper puisse construire la réponse
        var result = await db.Sessions
            .Include(s => s.Activities) 
            .Include(s => s.SessionAchievements)
            .ThenInclude(sa => sa.Achievement)
            .FirstAsync(s => s.Id == sessionToEdit.Id, ct);

        await Send.OkAsync(mapper.Map<GetSessionDto>(result), ct);
    }
}