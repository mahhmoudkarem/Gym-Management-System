using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities.Enums;

namespace DAL.Entities.Trainer
{
    public class Trainer:GymUser
    {
        // HireDate == CreatedAt from BaseEntity
        public Specialties Specialties { get; set; }

        #region Relations

        #region Session - Trainerr [1-M]

        public ICollection<Session.Session> Sessions { get; set; } = null!;

        #endregion

        #endregion
    }
}
