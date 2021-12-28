using AutoMapper;
using LitBlog.API.Models;
using LitBlog.BLL.ModelsDto;
using LitChat.API.Models;
using LitChat.BLL.ModelsDto;

namespace LitBlog.API.Helpers
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AccountDto, AccountViewModel>()
                .ForMember(x => x.MessagesFromUser, opt => opt.MapFrom(src => src.MessagesFromUser))
                .ForMember(x => x.MessagesToUser, opt => opt.MapFrom(src => src.MessagesToUser));
            CreateMap<AccountViewModel, UserInfoViewModel>()
                .ForMember(x => x.Phone, opt => opt.MapFrom(x => x.Phone))
                .ForMember(x => x.Description, opt => opt.MapFrom(x => x.Description))
                .ReverseMap();
            CreateMap<UserInfoDto, UserInfoViewModel>()
                .ForMember(x => x.Phone, opt => opt.MapFrom(x => x.Phone))
                .ForMember(x => x.Description, opt => opt.MapFrom(x => x.Description))
                .ReverseMap();
            CreateMap<ConnectionsDto, ConnectionViewModel>()
                .ForMember(x => x.ConnectedAt, opt => opt.MapFrom(src => src.ConnectedAt))
                .ForMember(x => x.DisconnectedAt, opt => opt.MapFrom(src => src.DisconnectedAt))
                .ReverseMap();
            CreateMap<ConnectionsDto, ConnectionsResponseViewModel>()
                .ForMember(x => x.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(x => x.UserAccount, opt => opt.MapFrom(src => src.UserAccount))
                .ForMember(x => x.IsOnline, opt => opt.MapFrom(src => src.IsOnline))
                .ForMember(x => x.ConnectedAt, opt => opt.MapFrom(src => src.ConnectedAt))
                .ForMember(x => x.DisconnectedAt, opt => opt.MapFrom(src => src.DisconnectedAt))
                .ReverseMap();
            CreateMap<AccountDto, AccountRegisterViewModel>().ReverseMap();
            CreateMap<AuthenticateResponseDto, AccountRegisterViewModel>();
            CreateMap<UsersResponseDto, UserResponseViewModel>();
            CreateMap<AuthenticateRequestDto, AuthenticateRequestViewModel>().ReverseMap();
            CreateMap<AuthenticateResponseDto, AuthenticateResponseViewModel>().ReverseMap();
            CreateMap<UpdateAccountViewModel, UpdateAccountDto>().ReverseMap();
            CreateMap<ResetPasswordRequestDto, ResetPasswordRequestViewModel>().ReverseMap();
            CreateMap<ForgotPasswordRequestDto, ForgotPasswordRequestViewModel>().ReverseMap();
            CreateMap<ChatMessagesDto, ChatMessageModel>()
                .ForMember(x => x.FromUserId, opt => opt.MapFrom(src => src.FromUserId))
                .ForMember(x => x.ToUserId, opt => opt.MapFrom(dst => dst.ToUserId))
                .ForMember(x => x.Message, opt => opt.MapFrom(src => src.Message))
                .ReverseMap();
            CreateMap<AccountDto, UserResponseViewModel>().ReverseMap();
            CreateMap<RoleDto, RoleViewModel>();
            CreateMap<FavoritesListViewModel, FavoritesListDto>();
            CreateMap<FavoritesListResponseDto, FavoritesListResponseViewModel>();
        }
    }
}
