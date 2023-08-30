using AutoMapper;
using MagicVilla_web.Models;
using MagicVilla_web.Models.DTO;

namespace MagicVilla_web
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Villa, VillaDTO>();
            CreateMap<VillaDTO, Villa>();

            CreateMap<Villa, VillaCreateDTO>().ReverseMap();
            CreateMap<Villa, VillaUpdateDTO>().ReverseMap();

            CreateMap<VillaNumber, VillaNumberDTO>();
            CreateMap<VillaNumberDTO, VillaNumber>();

            CreateMap<VillaNumber, VillaCreateNumberDTO>().ReverseMap();
            CreateMap<VillaNumber, VillaUpdateNumberDTO>().ReverseMap();
        }
    }
}
