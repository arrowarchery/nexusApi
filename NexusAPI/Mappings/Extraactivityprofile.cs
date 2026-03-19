using AutoMapper;
using NexusAPI.DTO.ExtraActivity.Response;
using NexusAPI.DTO.ExtraActivity.Request;
using NexusAPI.Models;

namespace NexusAPI.Mappings;

public class ExtraActivityProfile : Profile
{
    public ExtraActivityProfile()
    {
        // 1. Entité -> DTO (Lecture)
        CreateMap<ExtraActivity, GetExtraActivityDto>();

        // 2. DTO -> Entité (Création)
        CreateMap<CreateExtraActivityDto, ExtraActivity>();

        // 3. DTO -> Entité (Mise à jour)
        // On ignore l'Id pour garantir qu'on ne modifie pas la clé primaire par erreur
        CreateMap<UpdateExtraActivityDto, ExtraActivity>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }
}