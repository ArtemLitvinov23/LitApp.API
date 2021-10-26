using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LitBlog.API.Models;
using LitBlog.BLL.ModelsDto;

namespace LitBlog.API.Helpers
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AccountDto, AccountViewModel>();
            CreateMap<AccountDto, AccountResponseViewModel>();
            CreateMap<AccountDto, UserResponseViewModel>();
            CreateMap<AccountDto, AuthenticateRequestViewModel>();
            CreateMap<AccountDto, AuthenticateResponseViewModel>();
            CreateMap<UserResponseViewModel, UsersResponseDto>();
            CreateMap<UpdateAccountViewModel, AccountDto>()
                .ForAllMembers(x => x.Condition((src, dest, prop) =>
                {
                    if (prop == null) return false;
                    if (prop is string && string.IsNullOrEmpty((string)prop)) return true;
                    if (x.DestinationMember.Name == "Role" && src.Role == null) return false;
                    return true;
                }));
        }
    }
}
