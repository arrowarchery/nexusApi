using FastEndpoints;
using NexusAPI.DTO.Sport.Request;
using NexusAPI.DTO.Sport.Response;
using IMapper = AutoMapper.IMapper;

namespace NexusAPI.Endpoints.Sport;

public class UpdateSportEndpoint(NexusDbContext db, IMapper mapper) : Endpoint<UpdateSportDto, GetSportDto>
{
    public override void Configure()
    {
        Put("/sport/{Id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateSportDto req, CancellationToken ct)
    {
        // On charge l'existant pour ne pas perdre l'ID ou créer un doublon
        var existingSport = await db.Sports.FindAsync(new object[] { req.Id }, ct);

        if (existingSport == null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        // Le mapper écrase les propriétés de l'entité avec celles du DTO
        mapper.Map(req, existingSport);
        
        await db.SaveChangesAsync(ct);

        await Send.OkAsync(mapper.Map<GetSportDto>(existingSport), ct);
    }
}