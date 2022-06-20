using LitApp.DAL.Models.Enum;
using System;

namespace LitApp.DAL.Models
{
    public class Friend
    {
        public int Id { get; set; }

        public int RequestById { get; set; }
        public virtual Account RequestBy { get; set; }

        public int RequestToId { get; set; }
        public virtual Account RequestTo { get; set; }

        public DateTime? RequestTime { get; set; }

        public RequestFlags RequestFlags { get; set; }

        public bool Approved => RequestFlags == RequestFlags.Approved;

        public DateTime? ApprovedDate { get; set; }

        public DateTime? DateOfRejection { get; set; }

        public DateTime? NextRequest { get; set; }
    }
}
