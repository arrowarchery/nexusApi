using FastEndpoints;
using NexusAPI.DTO.Class.Request;
using NexusAPI.DTO.Class.Response;
using IMapper = AutoMapper.IMapper;

namespace NexusAPI.Endpoints.Class;

public class UpdateClassEndpoint(NexusDbContext db, IMapper mapper) : Endpoint<UpdateClassDto, GetClassDto>
{
    public override void Configure()
    {
        Put("/class/{Id}"); // Ajout de l'ID dans la route pour plus de clarté
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateClassDto req, CancellationToken ct)
    {
        var existingClass = await db.Classes.FindAsync(new object[] { req.Id }, ct);

        if (existingClass == null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        // Applique les modifs du DTO sur l'entité existante
        mapper.Map(req, existingClass);
        
        await db.SaveChangesAsync(ct);

        await Send.OkAsync(mapper.Map<GetClassDto>(existingClass), ct);
    }
}