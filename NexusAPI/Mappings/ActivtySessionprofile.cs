using AutoMapper;
using NexusAPI.DTO.ActivitySession.Response;
using NexusAPI.DTO.ActivitySession.Request;
using NexusAPI.Models;

namespace NexusAPI.Mappings;

public class ActivitySessionProfile : Profile
{
    public ActivitySessionProfile()
    {
        // 1. Entité -> DTO (Lecture)
        CreateMap<ActivitySession, GetActivitySessionDto>();

        // 2. DTO -> Entité (Création)
        CreateMap<ActivitySessionDto, ActivitySession>();
    }
}