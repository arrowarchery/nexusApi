using AutoMapper;
using NexusAPI.DTO.Class.Response;
using NexusAPI.DTO.Class.Request;
using NexusAPI.Models;

namespace NexusAPI.Mappings;

public class ClassProfile : Profile
{
    public ClassProfile()
    {
        // 1. Entité -> DTO (Lecture)
        CreateMap<Class, GetClassDto>();

        // 2. DTO -> Entité (Création)
        CreateMap<CreateClassDto, Class>();

        // 3. DTO -> Entité (Mise à jour)
        // On ignore l'Id pour empêcher toute modification de la clé primaire en base
        CreateMap<UpdateClassDto, Class>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }
}