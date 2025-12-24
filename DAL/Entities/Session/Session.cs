using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities.RelationalEntities;

namespace DAL.Entities.Session
{
    public class Session : BaseEntity
    {
        public string Descripcion { get; set; } = null!;
        public int Capacity { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }


        #region Relations

        #region Session - Category [1-M]

        public int CategoryId { get; set; }
        public Category.Category Category { get; set; } = null!;

        #endregion
        #region Session - Trainer [1-M]

        public int TrainerId { get; set; }
        public Trainer.Trainer Trainer { get; set; } = null!;

        #endregion
        #region Session - Member [M-M]
            public ICollection<MemberSession> MemberSessions { get; set; }
        #endregion



        #endregion

    }
}
