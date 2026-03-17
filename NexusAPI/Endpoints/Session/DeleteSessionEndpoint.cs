using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using NexusAPI.Models;

namespace NexusAPI.Endpoints.Session;

public class DeleteSessionRequest
{
    public int Id { get; set; }
}

public class DeleteSessionEndpoint(NexusDbContext db) : Endpoint<DeleteSessionRequest>
{
    public override void Configure()
    {
        Delete("/sessions/{id}", x => new { x.Id });
        AllowAnonymous();
    }

    public override async Task HandleAsync(DeleteSessionRequest req, CancellationToken ct)
    {
        // Récupérer la session avec ses liaisons
        var sessionToDelete = await db.Sessions
            .Include(s => s.SessionAchievements)
            .SingleOrDefaultAsync(s => s.Id == req.Id, ct);

        if (sessionToDelete is null)
        {
            Console.WriteLine($"Aucune session avec l'ID {req.Id} trouvée.");
            await Send.NotFoundAsync(ct);
            return;
        }

        // Supprimer toutes les liaisons avec les achievements
        if (sessionToDelete.SessionAchievements.Any())
        {
            db.SessionAchievements.RemoveRange(sessionToDelete.SessionAchievements);
        }

        // Supprimer la session
        db.Sessions.Remove(sessionToDelete);
        await db.SaveChangesAsync(ct);

        Console.WriteLine($"Session {req.Id} a été supprimée avec succès.");
        await Send.NoContentAsync(ct);
    }
}