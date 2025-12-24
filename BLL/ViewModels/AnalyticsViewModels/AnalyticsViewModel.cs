using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels.AnalyticsServices
{
    public class AnalyticsViewModel
    {
        public int TotalMember {  get; set; }
        public int ActiveMember { get; set; }
        public int TotalTrainers { get; set; }
        public int UpcomingSessions { get; set; }
        public int CompletedSessions { get; set; }
        public int OngoingSessions { get; set; }
    }
}
