using AutoMapper;
using NexusAPI.DTO.SessionAchievement.Response;
using NexusAPI.DTO.SessionAchievement.Request; // Ajout de l'using pour le Create
using NexusAPI.Models;

namespace NexusAPI.Mappings;

public class SessionAchievementProfile : Profile
{
    public SessionAchievementProfile()
    {
        // 1. Entité -> DTO (Lecture avec objets imbriqués)
        CreateMap<SessionAchievement, GetSessionAchievementDto>()
            .ForMember(dest => dest.Session, opt => opt.MapFrom(src => src.Session))
            .ForMember(dest => dest.Achievement, opt => opt.MapFrom(src => src.Achievement));

        // 2. DTO -> Entité (Création)
        CreateMap<SessionAchievementDto, SessionAchievement>();
    }
}