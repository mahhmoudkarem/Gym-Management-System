using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.ViewModels.AnalyticsServices;

namespace BLL.Services.AnalyticsServices
{
    public interface IAnalyticsService
    {
        AnalyticsViewModel GetAnalytics();
    }
}
