using NexusAPI.DTO.SmartReminder.Request;
using NexusAPI.DTO.SmartReminder.Response;
using FastEndpoints;
using IMapper = AutoMapper.IMapper;

namespace NexusAPI.Endpoints.SmartReminder;

public class CreateSmartReminderEndpoint(NexusDbContext db, IMapper mapper)
    : Endpoint<CreateSmartReminderDto, GetSmartReminderDto>
{
    public override void Configure()
    {
        Post("/smartreminders");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateSmartReminderDto req, CancellationToken ct)
    {
        // Mapping DTO -> Modèle
        var smartreminder = mapper.Map<Models.SmartReminder>(req);
        
        db.SmartReminders.Add(smartreminder);
        await db.SaveChangesAsync(ct);

        // On renvoie l'objet créé (mappé en DTO)
        await Send.OkAsync(mapper.Map<GetSmartReminderDto>(smartreminder), ct);
    }
}