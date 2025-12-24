using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities.HealthRecord
{
    // 1-1 relation with member
    public class HealthRecord:BaseEntity
    {
        public decimal Hight { get; set; }
        public decimal Wight { get; set; }
        public string BloodType { get; set; } = null!;
        public string? Note { get; set; }

        // LastUpdated == UpdatedAt from BaseEntity



    }
}
