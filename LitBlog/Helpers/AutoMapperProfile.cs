using AutoMapper;
using LitBlog.API.Models;
using LitBlog.BLL.ModelsDto;

namespace LitBlog.API.Helpers
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AccountDto, AccountRegisterViewModel>().ReverseMap();
            CreateMap<AuthenticateResponseDto, AccountRegisterViewModel>();
            CreateMap<UsersResponseDto, UserResponseViewModel>();
            CreateMap<AuthenticateRequestDto, AuthenticateRequestViewModel>().ReverseMap();
            CreateMap<AuthenticateResponseDto, AuthenticateResponseViewModel>().ReverseMap();
            CreateMap<UpdateAccountViewModel, UpdateAccountDto>().ReverseMap();
            CreateMap<ResetPasswordRequestDto, ResetPasswordRequestViewModel>().ReverseMap();
            CreateMap<ForgotPasswordRequestDto, ForgotPasswordRequestViewModel>().ReverseMap();
            CreateMap<ChatMessageDto, ChatMessageModel>().ReverseMap();
            CreateMap<ApplicationUserDto, ApplicationUserViewModel>().ReverseMap();
        }
    }
}
