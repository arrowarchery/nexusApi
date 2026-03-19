using NexusAPI.DTO.SmartReminder.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using IMapper = AutoMapper.IMapper;

namespace NexusAPI.Endpoints.SmartReminder;

public class GetSmartReminderRequest
{
    public int Id { get; set; }
}

public class GetSmartReminderEndpoint(NexusDbContext db, IMapper mapper)
    : Endpoint<GetSmartReminderRequest, GetSmartReminderDto>
{
    public override void Configure()
    {
        Get("/smartreminders/{Id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetSmartReminderRequest req, CancellationToken ct)
    {
        var smartReminder = await db.SmartReminders
            .SingleOrDefaultAsync(x => x.Id == req.Id, ct);

        if (smartReminder == null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        await Send.OkAsync(mapper.Map<GetSmartReminderDto>(smartReminder), ct);
    }
}