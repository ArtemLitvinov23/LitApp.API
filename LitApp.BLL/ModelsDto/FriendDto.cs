using LitApp.DAL.Models.Enum;
using System;

namespace LitApp.BLL.ModelsDto
{
    public class FriendDto
    {
        public FriendDto() {}
        public FriendDto(int requestById, int requestToId)
        {
            RequestById = requestById;
            RequestToId = requestToId;
            RequestFlags = RequestFlags.Pending;
            RequestTime = DateTime.UtcNow;
        }
        public int RequestById { get; set; }

        public int RequestToId { get; set; }

        public DateTime? RequestTime { get; set; }

        public RequestFlags RequestFlags { get; set; }

        public bool Approved => RequestFlags == RequestFlags.Approved;

        public DateTime? ApprovedDate { get; set; }

        public DateTime? DateOfRejection { get; set; }

        public DateTime? NextRequest { get; set; }

        public virtual AccountDto RequestTo { get; set; }

        public virtual AccountDto RequestBy { get; set; }
    }
}
