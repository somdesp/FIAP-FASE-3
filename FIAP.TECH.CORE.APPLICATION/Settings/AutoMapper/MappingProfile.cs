using AutoMapper;
using FIAP.TECH.CORE.APPLICATION.DTO;
using FIAP.TECH.CORE.DOMAIN.Entities;

namespace FIAP.TECH.CORE.APPLICATION.Settings.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ContactDto, Contact>().ReverseMap();
        CreateMap<Contact, ContactDetailsDto>();
        CreateMap<Contact, ContactUpdateDto>()
            .ReverseMap()
            .ForAllMembers(x => x.Condition(
                   (src, dest, prop) =>
                   {
                       if (prop == null) return false;
                       return true;
                   }));

        CreateMap<Region, RegionDetailsDto>();
    }
}
