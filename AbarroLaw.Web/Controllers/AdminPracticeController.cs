using AbarroLaw.Web.Models.Domain;
using AbarroLaw.Web.Models.ViewModels;
using AbarroLaw.Web.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AbarroLaw.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminPracticeController : Controller
    {
        private readonly IPracticeRepository practiceRepository;
        private readonly IWebHostEnvironment webHostEnvironment;

        public AdminPracticeController(IPracticeRepository practiceRepository, IWebHostEnvironment _webHostEnvironment)
        {
            this.practiceRepository = practiceRepository;
            webHostEnvironment = _webHostEnvironment;
        }

        //Add Practice ---------------------------------------------------------------------------------------
        [HttpGet]
        public IActionResult AddPractice()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SubmitPractice(AddPracticeRequest addPracticeRequest)
        {
            // Check if any required fields are null
            if (string.IsNullOrEmpty(addPracticeRequest.PracticeName) || string.IsNullOrEmpty(addPracticeRequest.PracticeDescription) || addPracticeRequest.PracticeImage == null || string.IsNullOrEmpty(addPracticeRequest.PracticeImageURL))
            {
                ModelState.AddModelError("", "Please fill out all required fields.");
                return View("AddPractice", addPracticeRequest); // Return to the same view with validation errors
            }
            else
            {
                var practiceModel = new Practice
                {
                    PracticeName = addPracticeRequest.PracticeName,
                    PracticeDescription = addPracticeRequest.PracticeDescription,
                    PracticeImage = addPracticeRequest.PracticeImage,
                    PracticeImageURL = addPracticeRequest.PracticeImageURL,
                    Visible = addPracticeRequest.Visible
                };


                await practiceRepository.AddPracticeAsync(practiceModel);

                return RedirectToAction("ViewAllPractice");

            }
        }



        //View All Practice ---------------------------------------------------------------------------------
        public async Task<IActionResult> ViewAllPractice()
        {
            var practice = await practiceRepository.GetAllPracticeAsync();
            return View(practice);

        }



        //Edit ----------------------------------------------------------------------------------------------
        [HttpGet]
        public async Task<IActionResult> EditPractice(Guid id)
        {
            var practice = await practiceRepository.GetPracticeAsync(id);

            if (practice != null)
            {
                var editPracticeRequest = new EditPracticeRequest
                {
                    Id = practice.Id,
                    PracticeName = practice.PracticeName,
                    PracticeDescription = practice.PracticeDescription,
                    PracticeImage = practice.PracticeImage,
                    PracticeImageURL = practice.PracticeImageURL,
                    Visible = practice.Visible
                };

                return View(editPracticeRequest);
            }

            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> EditPractice(EditPracticeRequest editPracticeRequest)
        {
            // Check if any required fields are null
            if (string.IsNullOrEmpty(editPracticeRequest.PracticeName) || string.IsNullOrEmpty(editPracticeRequest.PracticeDescription) || editPracticeRequest.PracticeImage == null || string.IsNullOrEmpty(editPracticeRequest.PracticeImageURL))
            {
                ModelState.AddModelError("", "Please fill out all required fields.");
                return View("EditPractice", editPracticeRequest); // Return to the same view with validation errors
            }
            else
            {
                var practice = new Practice
                {
                    Id = editPracticeRequest.Id,
                    PracticeName = editPracticeRequest.PracticeName,
                    PracticeDescription = editPracticeRequest.PracticeDescription,
                    PracticeImage = editPracticeRequest.PracticeImage,
                    PracticeImageURL = editPracticeRequest.PracticeImageURL,
                    Visible = editPracticeRequest.Visible
                };

                var updatePractice = await practiceRepository.UpdatePracticeAsync(practice);

                if (updatePractice != null)
                {
                    return RedirectToAction("ViewAllPractice");
                }
                else
                {
                    return RedirectToAction("EditPractice");
                }

            }            

        }



        //Delete --------------------------------------------------------------------------------------------
        public async Task<IActionResult> DeletePractice(EditPracticeRequest editPracticeRequest)
        {
            var deletePractice = await practiceRepository.DeletePracticeAsync(editPracticeRequest.Id);

            if (deletePractice != null)
            {
                return RedirectToAction("ViewAllPractice");
            }
            else
            {
                return RedirectToAction("EditPractice", new { id = editPracticeRequest.Id });
            }

        }
    }
}