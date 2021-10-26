using AutoMapper;
using LitBlog.BLL.ModelsDto;
using LitBlog.DAL.Models;

namespace LitBlog.BLL.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Account, AccountDto>().ReverseMap();
            CreateMap<Account, AccountResponseDto>();
            CreateMap<Account, UsersResponseDto>();
            CreateMap<Account, AuthenticateRequestDto>();
            CreateMap<Account, AuthenticateResponseDto>();
            CreateMap<Role, RoleDto>();
            CreateMap<UpdateAccountDto, Account>()
                .ForAllMembers(x=>x.Condition((src,dest,prop)=>
                {
                    if (prop == null) return false;
                    if (prop is string && string.IsNullOrEmpty((string) prop)) return true;
                    if (x.DestinationMember.Name == "Role" && src.Role == null) return false;
                    return true;
                }));
        }
    }
}
