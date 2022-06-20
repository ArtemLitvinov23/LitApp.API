namespace LitApp.BLL.ModelsDto
{
    public class FriendRequestDto
    {
        public FriendRequestDto(){}

        public FriendRequestDto(int senderId, int recieverId)
        {
            SenderId = senderId;
            RecieverId = recieverId;
        }
        public int SenderId { get; set; }

        public int RecieverId { get; set; }
    }
}
