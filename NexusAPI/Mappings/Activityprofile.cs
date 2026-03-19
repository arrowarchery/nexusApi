using AutoMapper;
using NexusAPI.DTO.Activity.Response;
using NexusAPI.DTO.Activity.Request;
using NexusAPI.Models;

namespace NexusAPI.Mappings;

public class ActivityProfile : Profile
{
    public ActivityProfile()
    {
        // 1. Entité -> DTO (Lecture)
        CreateMap<Activity, GetActivityDto>();
        // Note : Si ton DTO GetActivityDto a une propriété 'TypeLabel'
        // elle ne sera pas remplie automatiquement car elle n'existe pas dans le modèle.

        // 2. DTO -> Entité (Création)
        CreateMap<CreateActivityDto, Activity>();

        // 3. DTO -> Entité (Mise à jour)
        CreateMap<UpdateActivityDto, Activity>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }
}