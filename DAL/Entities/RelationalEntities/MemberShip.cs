using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities.MemberEntity;

namespace DAL.Entities.RelationalEntities
{
    public class MemberShip :BaseEntity
    {
        // StartDate == CreatedAt

        public DateTime EndDate { get; set; }

        public string Status
        {
            get
            {
                if (EndDate >= DateTime.Now)
                {
                    return "InActive";
                }
                else
                {
                    return "Active";
                }
            }
        }
        public int MemberId { get; set; } 

        public Member Member { get; set; } = null!;

        public int PlanId { get; set; }

        public Plan.Plan Plan { get; set; } = null!;


    }
}
