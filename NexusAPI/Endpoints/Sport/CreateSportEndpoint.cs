using FastEndpoints;
using NexusAPI.DTO.Sport.Request;
using NexusAPI.DTO.Sport.Response;
using IMapper = AutoMapper.IMapper;

namespace NexusAPI.Endpoints.Sport;

public class CreateSportEndpoint(NexusDbContext db, IMapper mapper) : Endpoint<CreateSportDto, GetSportDto>
{
    public override void Configure()
    {
        Post("/sport");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateSportDto req, CancellationToken ct)
    {
        // Mapping DTO -> Entité
        var sport = mapper.Map<Models.Sport>(req);
        
        db.Sports.Add(sport);
        await db.SaveChangesAsync(ct);
        
        // Mapping Entité -> DTO de réponse
        await Send.OkAsync(mapper.Map<GetSportDto>(sport), ct);
    }
}