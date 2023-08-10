using AutoMapper;
using TrainingAPI.Models.DTO;

namespace TrainingAPI
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Villa, VillaDTO>();
            CreateMap<VillaDTO, Villa>();
            CreateMap<Villa, VillaCreateDTO>();
            CreateMap<Villa, VillaUpdateDTO>();
        }
    }
}
