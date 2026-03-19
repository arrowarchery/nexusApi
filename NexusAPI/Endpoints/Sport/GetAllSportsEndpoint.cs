using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using IMapper = AutoMapper.IMapper;
using NexusAPI.DTO.Sport.Response;

namespace NexusAPI.Endpoints.Sport;

public class GetAllASportsEndpoint(NexusDbContext db, IMapper mapper) : EndpointWithoutRequest<List<GetSportDto>>
{
    public override void Configure()
    {
        Get("/sport");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var sports = await db.Sports.AsNoTracking().ToListAsync(ct);

        // Mapping automatique de la liste (gère Name, Description, Type, Place, etc.)
        var responseDto = mapper.Map<List<GetSportDto>>(sports);

        await Send.OkAsync(responseDto, ct);
    }
}