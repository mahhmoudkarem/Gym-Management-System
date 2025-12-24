using BLL.Services.Member;
using BLL.ViewModels.MemberViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gym_Management_System.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class MemberController : Controller
    {
        private readonly IMemberService memberService;

        public MemberController(IMemberService memberService)
        {
            this.memberService = memberService;
        }
        #region GetAllMembers

        public IActionResult Index()
        {
            var Members = memberService.GetAllMembers();
            return View(Members);
        }

        #endregion
        #region GetMemberDetails

        public IActionResult MemberDetails(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id Of Member Can't Be 0 Or Negative Number.";
                return RedirectToAction(nameof(Index));
            }
            var MemberDetails = memberService.GetMemberDetails(id);

            if (MemberDetails is null) {
                TempData["ErrorMessage"] = "Member Not Found.";
                return RedirectToAction(nameof(Index)); 
            }
            else return View(MemberDetails);


        }

        #endregion
        #region GetHealthRecordDetails

        public IActionResult HealthRecordDetails(int id)
        {
            if (id <= 0)
            {
                TempData["Error Message"] = "Id Of Member Can't Be 0 Or Negative Number.";
                return RedirectToAction(nameof(Index));
            }
            var HealthRecordDetails = memberService.GetHealthRecord(id);

            if (HealthRecordDetails is null)
            {
                TempData["Error Message"] = "HealthRecordDetails Not Found.";
                return RedirectToAction(nameof(Index));
            }
            else return View(HealthRecordDetails);


        }

        #endregion

        #region CreateMember

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateMember(CreateMemberViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("DataInvalid", "Check Data And Missing Field");
                return View(nameof(Create), viewModel);
            }

            bool Result = memberService.CreateMember(viewModel);
            if (Result) {
                TempData["SuccessMessage"] = "Member Created Successfully";
            }
            else
            {
                TempData["ErrorMessage"] = "Member Failed To Create , Check Phone And Email";
            }
            return RedirectToAction($"{nameof(Index)}");


        }

        #endregion
        #region Edit Member

        public IActionResult EditMember(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id Of Member Can't Be 0 Or Negative Number.";
                return RedirectToAction(nameof(Index));
            }
            var Member = memberService.UpdateToMemberDetails(id);
            if (Member is null) {
                TempData["ErrorMessage"] = "Member Not Found.";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(Member);
            }
        }
        [HttpPost]
        public IActionResult EditMember([FromRoute]int id , UpdateMemberViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var Result = memberService.UpdateMemberDetails(id , model);
            if(Result) TempData["SuccessMessage"] = "Member Updated Successfully";
            else TempData["ErrorMessage"] = "Member Failed To Update.";

            return RedirectToAction(nameof(Index));

        }

        #endregion
        #region DeleteMember

        public IActionResult Delete(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id Of Member Can't Be 0 Or Negative Number.";
                return RedirectToAction(nameof(Index));
            }
            var Member = memberService.GetMemberDetails(id);
            if (Member is null)
            {
                TempData["ErrorMessage"] = "Member Not Found.";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewBag.MemberId = id;
                ViewBag.MemberName = Member.Name;
                return View();
            }
        }

        [HttpPost]
        public IActionResult DeleteConfirmed([FromForm]int id) {
        
            var Result = memberService.DeleteMember(id);
            if (Result) TempData["SuccessMessage"] = "Member Removed Successfully";
            else TempData["ErrorMessage"] = "Member Failed To Remove.";

            return RedirectToAction(nameof(Index));

        }
        #endregion
    }
}
