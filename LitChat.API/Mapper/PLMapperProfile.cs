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
                .ForMember(x => x.MessagesToUser, opt => opt.MapFrom(src => src.MessagesToUser))
                .ForMember(x => x.SentFriendsRequest, opt => opt.MapFrom(src => src.SentFriendsRequest))
                .ForMember(x => x.RecievedFriendRequest, opt => opt.MapFrom(src => src.RecievedFriendRequest));

            CreateMap<AccountViewModel, UserInfoViewModel>()
                .ForMember(x => x.Phone, opt => opt.MapFrom(x => x.Profile.Phone))
                .ForMember(x => x.Description, opt => opt.MapFrom(x => x.Profile.Description))
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

            CreateMap<AccountResponseDto, AccountResponseViewModel>()
                .ForMember(x => x.FirstName, opt => opt.MapFrom(src => src.Profile.FirstName))
                .ForMember(x => x.LastName, opt => opt.MapFrom(src => src.Profile.LastName))
                .ForMember(x => x.Description, opt => opt.MapFrom(src => src.Profile.Description))
                .ForMember(x => x.Phone, opt => opt.MapFrom(src => src.Profile.Phone));

            CreateMap<AccountRegisterViewModel, AccountDto>()
                .ForPath(x => x.Profile.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForPath(x => x.Profile.LastName, opt => opt.MapFrom(src => src.LastName));

            CreateMap<AuthenticateResponseDto, AccountRegisterViewModel>();

            CreateMap<AuthenticateRequestDto, AuthenticateRequestViewModel>().ReverseMap();

            CreateMap<AuthenticateResponseDto, AuthenticateResponseViewModel>()
                .ForMember(x => x.Id, opt => opt.MapFrom(src => src.AccountId))
                .ForMember(x => x.Profile, opt => opt.MapFrom(src => src.Profile));

            CreateMap<UpdateAccountViewModel, UpdateAccountDto>()
                .ForMember(x => x.Profile, opt => opt.MapFrom(src => src.Profile));

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

            CreateMap<FavoritesListViewModel, FavoritesListDto>();

            CreateMap<FavoritesListResponseDto, FavoritesListResponseViewModel>();

            CreateMap<FriendDto, AccountResponseViewModel>()
                .ForMember(x => x.Id, opt => opt.MapFrom(src => src.RequestToId))
                .ForMember(x => x.FirstName, opt => opt.MapFrom(src => src.RequestTo.Profile.FirstName))
                .ForMember(x => x.LastName, opt => opt.MapFrom(src => src.RequestTo.Profile.LastName))
                .ForMember(x => x.Description, opt => opt.MapFrom(src => src.RequestTo.Profile.Description))
                .ForMember(x => x.Phone, opt => opt.MapFrom(src => src.RequestTo.Profile.Phone))
                .ForMember(x => x.Email, opt => opt.MapFrom(src => src.RequestTo.Email));

            CreateMap<FriendDto, FriendViewModel>()
                .ForPath(x => x.FriendId, opt => opt.MapFrom(src => src.RequestToId))
                .ForPath(x => x.Email, opt => opt.MapFrom(src => src.RequestTo.Email))
                .ForPath(x => x.FirstName, opt => opt.MapFrom(src => src.RequestTo.Profile.FirstName))
                .ForPath(x => x.LastName, opt => opt.MapFrom(src => src.RequestTo.Profile.LastName))
                .ForPath(x => x.Approved, opt => opt.MapFrom(src => src.Approved))
                .ForPath(x => x.RequestFlags, opt => opt.MapFrom(src => src.RequestFlags));

            CreateMap<FriendRequestViewModel, AccountDto>()
                .ForMember(x => x.Id, opt => opt.MapFrom(src => src.AccountId))
                .ForMember(x => x.Id, opt => opt.MapFrom(src => src.FriendAccountId));


        }
    }
}
