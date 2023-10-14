using AutoMapper;
using WEBTechnology.Project.Models;
using WEBTechnology.Project.ViewModels.User;

namespace WEBTechnology.API1.Contracts
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserCreateViewModel, User>()
                .ForMember(x => x.Email, opt => opt.MapFrom(c => c.Email))
                .ForMember(x => x.Name, opt => opt.MapFrom(c => c.Name));
            CreateMap<UserEditViewModel, User>(); 
        }
    }
}
