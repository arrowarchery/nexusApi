using AutoMapper;
using NexusAPI.DTO.Session.Response;
using NexusAPI.DTO.Session.Request;
using NexusAPI.Models;

namespace NexusAPI.Mappings;

public class SessionProfile : Profile
{
    public SessionProfile()
    {
        // 1. Entité -> DTO (Lecture)
        CreateMap<Session, GetSessionDto>()
            // Status est déjà un string dans le modèle, pas besoin de .ToString()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            // On mappe SessionAchievements (Modèle) vers Achievements (DTO)
            .ForMember(dest => dest.Achievements, opt => opt.MapFrom(src => src.SessionAchievements));

        // 2. DTO -> Entité (Création)
        CreateMap<CreateSessionDto, Session>()
            // On passe directement le string, plus besoin d'Enum.Parse
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status ?? "Planned"))
            .ForMember(dest => dest.SessionAchievements, opt => opt.Ignore());

        // 3. DTO -> Entité (Mise à jour)
        CreateMap<UpdateSessionDto, Session>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status ?? "Planned"))
            .ForMember(dest => dest.SessionAchievements, opt => opt.Ignore());
    }
}