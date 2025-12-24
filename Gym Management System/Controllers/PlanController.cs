using BLL.Services.Member;
using BLL.Services.PlanServices;
using BLL.ViewModels.MemberViewModels;
using BLL.ViewModels.PlanViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gym_Management_System.Controllers
{
    [Authorize]
    public class PlanController : Controller
    {
        private readonly IPlanService planService;

        public PlanController(IPlanService planService) {
            this.planService = planService;
        }
        #region GetAllPlans

        public IActionResult Index()
        {
            var Plans = planService.GetPlans();
            return View(Plans);
        }

        #endregion
        #region Details

        public IActionResult Details(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id Of Plan Can't Be 0 Or Negative Number.";
                return RedirectToAction(nameof(Index));
            }
            var PlanDetails = planService.GetPlanById(id);

            if (PlanDetails is null)
            {
                TempData["ErrorMessage"] = "Member Not Found.";
                return RedirectToAction(nameof(Index));
            }
            else return View(PlanDetails);
        }

        #endregion
        #region Edit

        public IActionResult Edit(int id)
        {
            if (id <= 0)
            {
                TempData["WrongData"] = "Id Of Plan Can't Be 0 Or Negative Number.";
                return RedirectToAction(nameof(Index));
            }
            var Plan = planService.GetPlanToUpdate(id);
            if (Plan is null)
            {
                TempData["ErrorMessage"] = "Plan Not Found.";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(Plan);
            }
        }
        [HttpPost]
        public IActionResult Edit([FromRoute] int id, UpdatePlanViewModel model)
        {
            if (!ModelState.IsValid)
            {
                
                TempData["WrongData"] = "Plan Failed To Update.";
                return View(model);
            }

                var Result = planService.UpDatePlan(id, model);
            if (Result) TempData["SuccessMessage"] = "Plan Updated Successfully";
            else TempData["ErrorMessage"] = "Plan Failed To Update.";

            return RedirectToAction(nameof(Index));

        }

        #endregion
        #region Delete
        [HttpPost]
        public IActionResult Activation([FromRoute]int id) { 
            var Result = planService.ToggledPlan(id);
            if (Result)
            {
                TempData["SuccessMessage"] = "Plan Status Changed";
            }
            else
            {
                TempData["ErrorMessage"] = "Plan Failed To Change Status.";
            }
            return RedirectToAction(nameof(Index));

        }

        #endregion
    }
}
