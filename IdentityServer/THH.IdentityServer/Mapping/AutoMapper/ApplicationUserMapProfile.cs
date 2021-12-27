
using AutoMapper;

using THH.IdentityServer.Dtos;
using THH.IdentityServer.Models;

namespace THH.IdentityServer.Mapping.AutoMapper
{
    public class ApplicationUserMapProfile : Profile
    {
        public ApplicationUserMapProfile()
        {
            CreateMap<ApplicationUser, ApplicationUserDto>().ReverseMap();
            CreateMap<ApplicationUser, SignUpViewModel>().ReverseMap();
            CreateMap<ApplicationUser, UpdateModel>().ReverseMap();
        }
    }
}
