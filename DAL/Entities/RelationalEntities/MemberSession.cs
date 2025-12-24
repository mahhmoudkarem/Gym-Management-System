using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities.MemberEntity;

namespace DAL.Entities.RelationalEntities
{
    public class MemberSession : BaseEntity
    {
        // BookingDate == CreatedAt
        public bool IsAttended { get; set; }
        public int MemberId { get; set; }

        public Member Member { get; set; } = null!;

        public int SessionId { get; set; }

        public Session.Session Session { get; set; } = null!;
    }
}
