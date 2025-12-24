using Microsoft.AspNetCore.Mvc;

namespace Gym_Management_System.Controllers
{
    public class MembershipController : Controller
    {
        public MembershipController()
        {
            
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
