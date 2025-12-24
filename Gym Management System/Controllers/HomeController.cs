using BLL.Services.AnalyticsServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gym_Management_System.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IAnalyticsService analyticsService;

        public HomeController(IAnalyticsService analyticsService)
        {
            this.analyticsService = analyticsService;
        }



        public IActionResult Index()
        {
            var Data = analyticsService.GetAnalytics();
            return View(Data);
        }




    }
}
