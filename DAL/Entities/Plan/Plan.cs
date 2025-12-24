using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities.RelationalEntities;

namespace DAL.Entities.Plan
{
    public class Plan : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int DurationDays { get; set; } 
        public decimal Price { get; set; } 
        public bool IsActive { get; set; }


        #region Relations

       


        #region Membership

        public ICollection<MemberShip> PlanMembers { get; set; } = null!;

        #endregion



        #endregion
    }
}
