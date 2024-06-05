using AbarroLaw.Web.Models.Domain;
using AbarroLaw.Web.Models.ViewModels;
using AbarroLaw.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AbarroLaw.Web.Controllers
{
    public class AdminCaseController : Controller
    {
        private readonly ICaseRepository caseRepository;
        private readonly IPracticeRepository practiceRepository;

        public AdminCaseController(ICaseRepository caseRepository, IPracticeRepository practiceRepository)
        {
            this.caseRepository = caseRepository;
            this.practiceRepository = practiceRepository;
        }

        //Add ----------------------------------------
        [HttpGet]
        public async Task<IActionResult> AddCase()
        {
            var practices = await practiceRepository.GetAllPracticeAsync();

            var model = new AddCaseRequest
            {
                Practices = practices.Select(x => new SelectListItem { Text = x.PracticeName, Value = x.Id.ToString() })
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitCase(AddCaseRequest addCaseRequest)
        {
            //Mapping Practices
            var selectedPractices = new List<Practice>();

            foreach (var selectedPracticeId in addCaseRequest.SelectedPractices)
            {
                var selectedPracticeGuid = Guid.Parse(selectedPracticeId);
                var existingPractice = await practiceRepository.GetPracticeAsync(selectedPracticeGuid);

                if (existingPractice != null)
                {
                    selectedPractices.Add(existingPractice);
                }
            }

            //Mapping input values to send to DB
            var casePost = new CasePost
            {
                CaseName = addCaseRequest.CaseName,
                Heading = addCaseRequest.Heading,
                Content = addCaseRequest.Content,
                ShortDescription = addCaseRequest.ShortDescription,
                FeaturedImg = addCaseRequest.FeaturedImg,
                FeaturedImgURL = addCaseRequest.FeaturedImgURL,
                PublishedDate = addCaseRequest.PublishedDate,
                Visible = addCaseRequest.Visible,
                Practices = selectedPractices
            };
                        
            //Call repository to save to DB
            await caseRepository.AddCaseAsync(casePost);

            return RedirectToAction("ViewAllCase");
        }


        //Read --------------------------------------
        public async Task<IActionResult> ViewAllCase()
        {
            var cases = await caseRepository.GetAllCaseAsync();

            return View(cases);
        }


        //Update ------------------------------------
        [HttpGet]
        public async Task<IActionResult> EditCase(Guid id)
        {
            var casePost = await caseRepository.GetCaseAsync(id);
            var practiceDomainModel = await practiceRepository.GetAllPracticeAsync();

            if (casePost != null)
            {
                var model = new EditCaseRequest
                {
                    Id = casePost.Id,
                    CaseName = casePost.CaseName,
                    Heading = casePost.Heading,
                    Content = casePost.Content,
                    ShortDescription = casePost.ShortDescription,
                    FeaturedImg = casePost.FeaturedImg,
                    FeaturedImgURL = casePost.FeaturedImgURL,
                    PublishedDate = casePost.PublishedDate,
                    Visible = casePost.Visible,
                    Practices = practiceDomainModel.Select(x => new SelectListItem
                    {
                        Text = x.PracticeName,
                        Value = x.Id.ToString()
                    }),
                    SelectedPractices = casePost.Practices.Select(x => x.Id.ToString()).ToArray()
                };

                return View(model);
            }
            else
            {
                return View(null);
            }

        }

        [HttpPost]
        public async Task<IActionResult> EditCase(EditCaseRequest editCaseRequest)
        {
            //Assign values to edit request
            var casePostModel = new CasePost
            {
                Id = editCaseRequest.Id,
                CaseName = editCaseRequest.CaseName,
                Heading = editCaseRequest.Heading,
                Content = editCaseRequest.Content,
                ShortDescription = editCaseRequest.ShortDescription,
                FeaturedImg = editCaseRequest.FeaturedImg,
                FeaturedImgURL = editCaseRequest.FeaturedImgURL,
                PublishedDate = editCaseRequest.PublishedDate,
                Visible = editCaseRequest.Visible
            };

            //Mapping practices
            var selectedPractices = new List<Practice>();
            foreach (var selectedPractice in editCaseRequest.SelectedPractices)
            {
                if (Guid.TryParse(selectedPractice, out var practice))
                {
                    var foundPractice = await practiceRepository.GetPracticeAsync(practice);
                    if (foundPractice != null)
                    {
                        selectedPractices.Add(foundPractice);
                    }
                }
            }

            casePostModel.Practices = selectedPractices;

            //Apply changes to DB
            var updateCase = await caseRepository.UpdateCaseAsync(casePostModel);

            if (updateCase != null)
            {
                return RedirectToAction("ViewAllCase");
            }
            else
            {
                return RedirectToAction("EditCase");
            }
        }


        //Delete -------------------------------------
        public async Task<IActionResult> DeleteCaseAsync(EditCaseRequest editCaseRequest)
        {
            var deleteCase = await caseRepository.DeleteCaseAsync(editCaseRequest.Id);

            if (deleteCase != null)
            {
                return RedirectToAction("ViewAllCase");
            }
            else
            {
                return RedirectToAction("EditCase", new { id = editCaseRequest.Id } );
            }
        }


    }
}
