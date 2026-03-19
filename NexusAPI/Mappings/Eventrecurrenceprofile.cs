using AutoMapper;
using NexusAPI.DTO.EventRecurrence.Response;
using NexusAPI.DTO.EventRecurrence.Request; // Ajouté pour Create/Update
using NexusAPI.Models;

namespace NexusAPI.Mappings;

public class EventRecurrenceProfile : Profile
{
    public EventRecurrenceProfile()
    {
        // 1. Entité -> DTO (Lecture)
        CreateMap<EventRecurrence, GetEventRecurrenceDto>();

        // 2. DTO -> Entité (Création)
        CreateMap<CreateEventRecurrenceDto, EventRecurrence>();

        // 3. DTO -> Entité (Mise à jour)
        // On ignore l'Id pour garantir l'intégrité de la clé primaire
        CreateMap<UpdateEventRecurrenceDto, EventRecurrence>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }
}