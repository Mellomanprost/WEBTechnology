using AutoMapper;
using WEBTechnology.BLL.ViewModels.User;
using WEBTechnology.DAL.Models;

namespace WEBTechnology.API.Contracts
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserCreateViewModel, User>()
                .ForMember(x => x.Name, opt => opt.MapFrom(c => c.Name))
                .ForMember(x => x.Age, opt => opt.MapFrom(c => c.Age))
                .ForMember(x => x.Email, opt => opt.MapFrom(c => c.Email));
            CreateMap<UserEditViewModel, User>();
        }
    }
}
