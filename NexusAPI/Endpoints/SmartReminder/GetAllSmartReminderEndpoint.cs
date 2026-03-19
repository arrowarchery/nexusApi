using NexusAPI.DTO.SmartReminder.Response;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using IMapper = AutoMapper.IMapper;

namespace NexusAPI.Endpoints.Session;

public class GetAllSmartReminderEndpoint(NexusDbContext db, IMapper mapper)
    : EndpointWithoutRequest<List<GetSmartReminderDto>>
{
    public override void Configure()
    {
        Get("/smartreminders");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var reminders = await db.SmartReminders.AsNoTracking().ToListAsync(ct);

        // Mapping de la liste entière
        var responseDto = mapper.Map<List<GetSmartReminderDto>>(reminders);

        await Send.OkAsync(responseDto, ct);
    }
}