using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using NexusAPI.DTO.Sport.Response;
using IMapper = AutoMapper.IMapper;

namespace NexusAPI.Endpoints.Sport;

public class GetSportEndpoint(NexusDbContext db, IMapper mapper) : Endpoint<GetSportDto>
{
    public override void Configure()
    {
        Get("/sport/{Id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetSportDto req, CancellationToken ct)
    {
        var sport = await db.Sports
            .SingleOrDefaultAsync(a => a.Id == req.Id, ct);

        if (sport == null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        await Send.OkAsync(mapper.Map<GetSportDto>(sport), ct);
    }
}