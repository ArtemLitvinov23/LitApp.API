using AutoMapper;
using LitChat.API.Models;
using LitChat.BLL.ModelsDto;

namespace LitChat.API.Mapper
{
    public class PLMapperProfile : Profile
    {
        public PLMapperProfile()
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
            CreateMap<ConnectionsResponseDto, ConnectionsResponseViewModel>()
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
                .ForMember(x => x.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate))
                .ForMember(x => x.FromEmail, opt => opt.MapFrom(src => src.FromEmail))
                .ForMember(x => x.ToEmail, opt => opt.MapFrom(src => src.ToEmail))
                .ForMember(x => x.Message, opt => opt.MapFrom(src => src.Message))
                .ReverseMap();
            CreateMap<ChatMessagesDto, ChatMessagesResponseViewModel>()
                .ForMember(x => x.MessageId, opt => opt.MapFrom(src => src.Id))
                .ForMember(x => x.FromUserId, opt => opt.MapFrom(src => src.FromUserId))
                .ForMember(x => x.ToUserId, opt => opt.MapFrom(dst => dst.ToUserId))
                .ForMember(x => x.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate))
                .ForMember(x => x.FromEmail, opt => opt.MapFrom(src => src.FromEmail))
                .ForMember(x => x.ToEmail, opt => opt.MapFrom(src => src.ToEmail))
                .ForMember(x => x.Message, opt => opt.MapFrom(src => src.Message))
                .ReverseMap();
            CreateMap<AccountDto, UserResponseViewModel>().ReverseMap();
            CreateMap<RoleDto, RoleViewModel>();
            CreateMap<FavoritesListViewModel, FavoritesListDto>();
            CreateMap<FavoritesListResponseDto, FavoritesListResponseViewModel>();
        }
    }
}
