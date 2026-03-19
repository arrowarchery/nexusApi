using AutoMapper;
using NexusAPI.DTO.SmartReminder.Response;
using NexusAPI.DTO.SmartReminder.Request;
using NexusAPI.Models;

namespace NexusAPI.Mappings;

public class SmartReminderProfile : Profile
{
    public SmartReminderProfile()
    {
        // 1. Entité -> DTO (Lecture)
        CreateMap<SmartReminder, GetSmartReminderDto>();
        // Plus besoin de .ForMember avec .ToString() car Status est déjà un string

        // 2. DTO -> Entité (Création)
        CreateMap<CreateSmartReminderDto, SmartReminder>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status ?? "Active"));

        // 3. DTO -> Entité (Mise à jour)
        CreateMap<UpdateSmartReminderDto, SmartReminder>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status ?? "Active"));
    }
}