using BLL.Services.Member;
using BLL.Services.TrainerServices;
using BLL.ViewModels.MemberViewModels;
using BLL.ViewModels.TrainerViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gym_Management_System.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class TrainerController : Controller
    {
        private readonly ITrainerService trainerService;

        public TrainerController(ITrainerService trainerService)
        {
            this.trainerService = trainerService;
        }
        #region GetAllTrainers

        public IActionResult Index()
        {
            var Trainers = trainerService.GetAllTrainers();
            return View(Trainers);
        }

        #endregion
        #region GetTrainerDetails

        public IActionResult TrainerDetails(int id)
        {
            if (id <= 0) {

                TempData["ErrorMessage"] = "Id Of Trainer Can't Be 0 Or Negative Number.";
                return RedirectToAction(nameof(Index));

            }

            var TrainerData = trainerService.GetTrainerDetails(id);
            if (TrainerData is  null)
            {
                TempData["ErrorMessage"] = "Trainer Not Found.";
                return RedirectToAction(nameof(Index));
            }else return View(TrainerData);
        }

        #endregion
        #region CreateTrainer

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateTrainerViewModel createTrainer)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("DataInvalid", "Check Data And Missing Field");
                return View(nameof(Create), createTrainer);
            }
            bool Result = trainerService.CreateTrainer(createTrainer);
            if (Result)
            {
                TempData["SuccessMessage"] = "Trainer Created Successfully";
            }
            else
            {
                TempData["ErrorMessage"] = "Trainer Failed To Create , Check Phone And Email";
            }
            return RedirectToAction($"{nameof(Index)}");
        }


        #endregion
        #region EditTrainer

        public IActionResult Edit(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id Of Trainer Can't Be 0 Or Negative Number.";
                return RedirectToAction(nameof(Index));
            }
            var Trainer = trainerService.GetTrainerToUpdated(id);
            if (Trainer is null)
            {
                TempData["ErrorMessage"] = "Trainer Not Found.";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(Trainer);
            }
        }
        [HttpPost]
        public IActionResult Edit([FromRoute] int id, UpdateTrainerViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var Result = trainerService.UpdateTrainer(id, model);
            if (Result) TempData["SuccessMessage"] = "Trainer Updated Successfully";
            else TempData["ErrorMessage"] = "Trainer Failed To Update.";

            return RedirectToAction(nameof(Index));

        }
        #endregion

        #region DeleteTrainer

        


        public IActionResult Delete(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id Of Trainer Can't Be 0 Or Negative Number.";
                return RedirectToAction(nameof(Index));
            }
            var Trainer = trainerService.GetTrainerDetails(id);
            if (Trainer is null)
            {
                TempData["ErrorMessage"] = "Trainer Not Found.";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewBag.TrainerId = id;
                ViewBag.TrainerName = Trainer.Name;
                return View();
            
        }
    }

        [HttpPost]
        public IActionResult DeleteConfirmed([FromForm] int id)
        {

            var Result = trainerService.DeleteTrainer(id);
            if (Result) TempData["SuccessMessage"] = "Trainer Removed Successfully";
            else TempData["ErrorMessage"] = "Trainer Failed To Remove.";

            return RedirectToAction(nameof(Index));

        }

        #endregion
    }
}
