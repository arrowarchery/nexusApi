using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using NexusAPI.DTO.Activity.Response;
using IMapper = AutoMapper.IMapper;

namespace NexusAPI.Endpoints.Activity;

public class GetAllActivitiesEndpoint(NexusDbContext db, IMapper mapper) 
    : EndpointWithoutRequest<List<GetActivityDto>>
{
    public override void Configure()
    {
        Get("/activity");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var activities = await db.Activities.ToListAsync(ct);

        // 1. On mappe la liste de base
        var responseDto = mapper.Map<List<GetActivityDto>>(activities);

        // 2. On enrichit les champs calculés (TypeLabel et Room)
        // On fait une boucle ou un Zip pour ne pas perdre la logique polymorphique
        for (int i = 0; i < activities.Count; i++)
        {
            var a = activities[i];
            var dto = responseDto[i];

            dto.Room = a switch
            {
                Models.Class c => c.Room,
                Models.Sport s => s.Place,
                Models.ExtraActivity e => e.Place,
                _ => "Nexus Zone"
            };

            dto.TypeLabel = a switch
            {
                Models.Class _ => "Cours",
                Models.Sport _ => "Sport",
                Models.ExtraActivity _ => "Extra",
                _ => "Activité"
            };
        }

        await Send.OkAsync(responseDto, ct);
    }
}