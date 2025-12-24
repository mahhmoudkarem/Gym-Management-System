using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities.Category
{
    public class Category : BaseEntity
    {
        public string CategoryName { get; set; } = null!;

        #region Relations

        #region Session - Category [1-M]

        public ICollection<Session.Session> Sessions { get; set; } = null!;

        #endregion


        #endregion
    }
}
