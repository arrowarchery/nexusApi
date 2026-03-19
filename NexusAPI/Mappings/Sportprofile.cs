using AutoMapper;
using NexusAPI.DTO.Sport.Response;
using NexusAPI.DTO.Sport.Request;
using NexusAPI.Models;

namespace NexusAPI.Mappings;

public class SportProfile : Profile
{
    public SportProfile()
    {
        // 1. Entité -> DTO (Lecture)
        CreateMap<Sport, GetSportDto>();
        // Plus besoin de .ForMember avec .ToString() car ce sont déjà des strings

        // 2. DTO -> Entité (Création)
        CreateMap<CreateSportDto, Sport>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type ?? "Other"))
            .ForMember(dest => dest.Intensity, opt => opt.MapFrom(src => src.Intensity ?? "Medium"));

        // 3. DTO -> Entité (Mise à jour)
        CreateMap<UpdateSportDto, Sport>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type ?? "Other"))
            .ForMember(dest => dest.Intensity, opt => opt.MapFrom(src => src.Intensity ?? "Medium"));
    }
}