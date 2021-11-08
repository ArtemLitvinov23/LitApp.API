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
            CreateMap<AccountDto, AuthenticateResponseDto>();
            CreateMap<Account, AccountResponseDto>();
            CreateMap<Account, ApplicationUserDto>();
            CreateMap<Account, UsersResponseDto>();
            CreateMap<Account, AuthenticateRequestDto>();
            CreateMap<Account, AuthenticateResponseDto>();
            CreateMap<Role, RoleDto>();
            CreateMap<UpdateAccountDto, Account>();
            CreateMap<AccountDto, ResetPasswordRequestDto>();
            CreateMap<ResetPasswordRequestDto, object>().ReverseMap();
            CreateMap<ForgotPasswordRequestDto, object>().ReverseMap();
            CreateMap<ChatMessage, ChatMessageDto>();

        }
    }
}
