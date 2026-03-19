using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using NexusAPI.DTO.Class.Response;
using IMapper = AutoMapper.IMapper;

namespace NexusAPI.Endpoints.Class;

public class GetAllClassesEndpoint(NexusDbContext db, IMapper mapper) : EndpointWithoutRequest<List<GetClassDto>>
{
    public override void Configure()
    {
        Get("/class");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var classes = await db.Classes.AsNoTracking().ToListAsync(ct);

        // Mapping de la liste entière en une seule ligne
        var responseDto = mapper.Map<List<GetClassDto>>(classes);

        await Send.OkAsync(responseDto, ct);
    }
}