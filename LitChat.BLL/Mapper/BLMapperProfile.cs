using AutoMapper;
using LitChat.BLL.ModelsDto;
using LitChat.DAL.Models;

namespace LitChat.BLL.Mapper
{
    public class BLMapperProfile : Profile
    {
        public BLMapperProfile()
        {
            CreateMap<object, AccountDto>();
            CreateMap<AccountDto, Account>()
                .ForMember(x => x.MessagesFromUser, opt => opt.MapFrom(src => src.MessagesFromUser))
                .ForMember(x => x.MessagesToUser, opt => opt.MapFrom(src => src.MessagesToUser))
                .ReverseMap();
            CreateMap<AccountDto, AuthenticateResponseDto>();
            CreateMap<Account, AccountResponseDto>();
            CreateMap<Account, UsersResponseDto>();
            CreateMap<Account, AuthenticateRequestDto>();
            CreateMap<Account, AuthenticateResponseDto>();
            CreateMap<Account, UserInfoDto>()
                .ForMember(x => x.Phone, opt => opt.MapFrom(x => x.Phone))
                .ForMember(x => x.Description, opt => opt.MapFrom(x => x.Description))
                .ReverseMap();
            CreateMap<Role, RoleDto>();
            CreateMap<UpdateAccountDto, Account>();
            CreateMap<AccountDto, ResetPasswordRequestDto>();
            CreateMap<ResetPasswordRequestDto, object>().ReverseMap();
            CreateMap<ForgotPasswordRequestDto, object>().ReverseMap();
            CreateMap<object, ChatMessagesDto>();
            CreateMap<ChatMessagesDto, ChatMessages>()
                .ForMember(x => x.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(x => x.FromUserId, opt => opt.MapFrom(src => src.FromUserId))
                .ForMember(x => x.ToUserId, opt => opt.MapFrom(dst => dst.ToUserId))
                .ForMember(x => x.FromEmail, opt => opt.MapFrom(src => src.FromEmail))
                .ForMember(x => x.ToEmail, opt => opt.MapFrom(src => src.ToEmail))
                .ForMember(x => x.Message, opt => opt.MapFrom(src => src.Message))
                .ReverseMap();
            CreateMap<object, ChatMessages>();
            CreateMap<FavoritesListDto, FavoritesList>().ReverseMap();
            CreateMap<FavoritesList, FavoritesListResponseDto>();
            CreateMap<Connections, ConnectionsResponseDto>()
                .ForMember(x => x.UserAccount, opt => opt.MapFrom(src => src.UserAccount))
                .ForMember(x => x.ConnectedAt, opt => opt.MapFrom(src => src.ConnectedAt))
                .ForMember(x => x.DisconnectedAt, opt => opt.MapFrom(src => src.DisconnectedAt))
                .ForMember(x => x.IsOnline, opt => opt.MapFrom(src => src.IsOnline))
                .ReverseMap();
            CreateMap<Connections, ConnectionsDto>().ReverseMap();
        }
    }
}
