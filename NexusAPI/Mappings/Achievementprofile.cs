using AutoMapper;
using NexusAPI.DTO.Achievement.Response;
using NexusAPI.DTO.Achievement.Request;
using NexusAPI.Models;

namespace NexusAPI.Mappings;

public class AchievementProfile : Profile
{
    public AchievementProfile()
    {
        // 1. Entité -> DTO (Lecture)
        CreateMap<Achievement, GetAchievementDto>()
            // On mappe SessionAchievements (Modèle) vers Sessions (DTO)
            .ForMember(dest => dest.Sessions, opt => opt.MapFrom(src => src.SessionAchievements));

        // 2. DTO -> Entité (Création)
        CreateMap<CreateAchievementDto, Achievement>()
            // On ignore la collection de navigation pour gérer les IDs manuellement
            .ForMember(dest => dest.SessionAchievements, opt => opt.Ignore());

        // 3. DTO -> Entité (Mise à jour)
        CreateMap<UpdateAchievementDto, Achievement>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.SessionAchievements, opt => opt.Ignore());
    }
}