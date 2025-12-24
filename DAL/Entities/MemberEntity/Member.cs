using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities.RelationalEntities;

namespace DAL.Entities.MemberEntity
{
    public class Member : GymUser
    {
        // JoinDate == CreatedAt from BaseEntity

        public string PhotoUrl { get; set; } = null!;

        #region Relations

        #region [HealthRecord]

        public HealthRecord.HealthRecord HealthRecord { get; set; } = null!;

        #endregion


        #region [Membership]

        public ICollection<MemberShip> MemberShips { get; set; } = null!;

        #endregion

        #region [MemberSession]

        public ICollection<MemberSession> MemberSessions { get; set; }

        #endregion



        #endregion
    }
}
