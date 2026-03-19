using NexusAPI.DTO.Achievement.Request;
using NexusAPI.DTO.Achievement.Response;
using FastEndpoints;
using IMapper = AutoMapper.IMapper;

namespace NexusAPI.Endpoints.Achievement;

public class CreateAchievementEndpoint(NexusDbContext db, IMapper mapper)
    : Endpoint<CreateAchievementDto, GetAchievementDto>
{
    public override void Configure()
    {
        Post("/achievements");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateAchievementDto req, CancellationToken ct)
    {
        // Utilisation du mapper pour transformer le DTO en Modèle
        var achievement = mapper.Map<Models.Achievement>(req);
        
        db.Achievements.Add(achievement);
        await db.SaveChangesAsync(ct);

        // Pas besoin de charger les sessions ici (un nouvel achievement n'en a pas encore)
        var response = mapper.Map<GetAchievementDto>(achievement);
        
        await Send.OkAsync(response, ct);
    }
}