using System.Runtime.Intrinsics.X86;
using BLL.Services.Member;
using BLL.Services.SessionServices;
using BLL.ViewModels.MemberViewModels;
using BLL.ViewModels.SessionViewModels;
using GymManagementSystemBLL.ViewModels.SessionViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Gym_Management_System.Controllers
{
    [Authorize]
    public class SessionController : Controller
    {
        private readonly ISessionService sessionService;

        public SessionController(ISessionService sessionService)
        {
            this.sessionService = sessionService;
        }


        #region GetAllSessions
        public IActionResult Index()
        {
            var Sessions = sessionService.GetAllSessions();
            return View(Sessions);
        }
        #endregion

        #region SessionDetails

        public IActionResult SessionDetails(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id Of Session Can't Be 0 Or Negative Number.";
                return RedirectToAction(nameof(Index));
            }
            var SessionDetails = sessionService.GetSessionById(id);

            if (SessionDetails is null)
            {
                TempData["ErrorMessage"] = "Session Not Found.";
                return RedirectToAction(nameof(Index));
            }
            else return View(SessionDetails);


        }

        #endregion

        #region CreateSession

        public IActionResult Create()
        {

            LoadDropDown();
            return View();

        }
        [HttpPost]
        public IActionResult Create(CreateSessionViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                LoadDropDown();
                return View(viewModel);
            }

            var result = sessionService.CreateSession(viewModel);

            if (!result)
            {
                TempData["ErrorMessage"] = "Session creation failed!";
                return RedirectToAction(nameof(Create));
            }

            TempData["SuccessMessage"] = "Session Created Successfully!";
            return RedirectToAction(nameof(Index));
        }



        #endregion

        #region Edit

        public IActionResult Edit(int id)
        {
            LoadDropDown();
            var SessionDetails = sessionService.GetSessionToUpdate(id);
            if (SessionDetails is null) return RedirectToAction(nameof(Index));
            else return View(SessionDetails);

        }
        [HttpPost] 
        public IActionResult Edit([FromRoute] int id, UpdateSessionViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var Result = sessionService.UpdateSession(model, id);
            if (Result) TempData["SuccessMessage"] = "Member Updated Successfully";
            else TempData["ErrorMessage"] = "Member Failed To Update.";

            return RedirectToAction(nameof(Index));

        }
        #endregion

        #region Delete

        public IActionResult Delete(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id Of Member Can't Be 0 Or Negative Number.";
                return RedirectToAction(nameof(Index));
            }
            var Session = sessionService.GetSessionById(id);
            if (Session is null)
            {
                TempData["ErrorMessage"] = "Session Not Found.";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewBag.SessionId = id;
                return View();
            }
        }

        [HttpPost]
        public IActionResult DeleteConfirmed([FromForm] int id)
        {

            var Result = sessionService.DeleteSession(id);
            if (Result) TempData["SuccessMessage"] = "Session Removed Successfully";
            else TempData["ErrorMessage"] = "Session Failed To Remove.";

            return RedirectToAction(nameof(Index));

        }
        #endregion


        #region Helper
        public void LoadDropDown()
        {
            var Categories = sessionService.GetCategoryForDropDown();
            ViewBag.Categories = new SelectList(Categories, "Id", "CategoryName");
            var Trainers = sessionService.GetTrainerForDropDown();
            ViewBag.Trainer = new SelectList(Trainers, "Id", "Name");
        }
        #endregion
    }
    }
