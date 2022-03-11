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

            CreateMap<UserInfoDto, UserInfo>()
                .ForMember(x => x.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(x => x.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(x => x.Phone, opt => opt.MapFrom(x => x.Phone))
                .ForMember(x => x.Description, opt => opt.MapFrom(x => x.Description)).ReverseMap();

            CreateMap<AccountDto, Account>()
                .ForMember(x => x.Profile, opt => opt.MapFrom(x => x.Profile))
                .ForMember(x => x.MessagesFromUser, opt => opt.MapFrom(src => src.MessagesFromUser))
                .ForMember(x => x.MessagesToUser, opt => opt.MapFrom(src => src.MessagesToUser))
                .ForMember(x => x.SentFriendsRequest, opt => opt.MapFrom(src => src.SentFriendsRequest))
                .ForMember(x => x.RecievedFriendRequest, opt => opt.MapFrom(src => src.RecievedFriendRequest)).ReverseMap();

            CreateMap<AccountDto, AuthenticateResponseDto>()
                .ForMember(x => x.AccountId, opt => opt.MapFrom(src => src.Id))
                .ForMember(x => x.Profile, opt => opt.MapFrom(src => src.Profile))
                .ForMember(x => x.TokenExpires, opt => opt.MapFrom(src => src.TokenExpires));

            CreateMap<Account, AccountResponseDto>()
                .ForMember(x => x.Profile, opt => opt.MapFrom(src => src.Profile))
                .ForMember(x => x.TokenExpires, opt => opt.MapFrom(src => src.TokenExpires));

            CreateMap<Account, AuthenticateRequestDto>();

            CreateMap<Account, AuthenticateResponseDto>()
                .ForMember(x => x.Profile, opt => opt.MapFrom(src => src.Profile));


            CreateMap<Account, UserInfoDto>()
                .ForMember(x => x.FirstName, opt => opt.MapFrom(src => src.Profile.FirstName))
                .ForMember(x => x.LastName, opt => opt.MapFrom(src => src.Profile.LastName))
                .ForMember(x => x.Phone, opt => opt.MapFrom(x => x.Profile.Phone))
                .ForMember(x => x.Description, opt => opt.MapFrom(x => x.Profile.Description))
                .ReverseMap();


            CreateMap<UpdateAccountDto, Account>()
                .ForMember(x => x.Profile, opt => opt.MapFrom(src => src.Profile));

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

            CreateMap<ConnectionsResponseDto, ConnectionsDto>().ReverseMap();

            CreateMap<Friend, AccountResponseDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(src => src.RequestToId))
                .ForMember(x => x.Email, opt => opt.MapFrom(src => src.RequestTo.Email))
                .ForMember(x => x.Profile, opt => opt.MapFrom(src => src.RequestTo.Profile));

            CreateMap<Friend, FriendDto>()
                .ForMember(x => x.RequestTo, opt => opt.MapFrom(src => src.RequestTo))
                .ReverseMap();
        }
    }
}
