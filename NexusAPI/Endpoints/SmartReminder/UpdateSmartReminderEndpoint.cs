using NexusAPI.DTO.SmartReminder.Request;
using NexusAPI.DTO.SmartReminder.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using IMapper = AutoMapper.IMapper;

namespace NexusAPI.Endpoints.SmartReminder;

public class UpdateSmartReminderEndpoint(NexusDbContext db, IMapper mapper)
    : Endpoint<UpdateSmartReminderDto, GetSmartReminderDto>
{
    public override void Configure()
    {
        Put("/smartreminders/{Id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateSmartReminderDto req, CancellationToken ct)
    {
        var smartreminderToEdit = await db.SmartReminders
            .SingleOrDefaultAsync(x => x.Id == req.Id, ct);

        if (smartreminderToEdit == null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        // Le mapper met à jour l'entité existante
        mapper.Map(req, smartreminderToEdit);

        await db.SaveChangesAsync(ct);

        await Send.OkAsync(mapper.Map<GetSmartReminderDto>(smartreminderToEdit), ct);
    }
}