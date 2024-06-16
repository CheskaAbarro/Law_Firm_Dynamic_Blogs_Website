using AbarroLaw.Web.Models.Domain;
using AbarroLaw.Web.Models.ViewModels;
using AbarroLaw.Web.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AbarroLaw.Web.Controllers
{

    [Authorize(Roles = "Admin")]
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
            if (addCaseRequest.CaseName == null || addCaseRequest.Heading == null || addCaseRequest.ShortDescription == null || addCaseRequest.FeaturedImg == null || addCaseRequest.Content == null)
            {
                // Re-populate Practices in case of a validation error
                var practices = await practiceRepository.GetAllPracticeAsync();
                addCaseRequest.Practices = practices.Select(x => new SelectListItem { Text = x.PracticeName, Value = x.Id.ToString() });

                //For error handling and returning to AddCase without removing the existing input
                ModelState.AddModelError("", "Please fill out this form.");
                return View("AddCase", addCaseRequest); // Return to the form with validation error
            }
            else
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

                var UrlHandleMaker = GenerateUrlHandle(addCaseRequest.Heading);

                //Mapping input values to send to DB
                var casePost = new CasePost
                {
                    CaseName = addCaseRequest.CaseName,
                    Heading = addCaseRequest.Heading,
                    Content = addCaseRequest.Content,
                    ShortDescription = addCaseRequest.ShortDescription,
                    FeaturedImg = addCaseRequest.FeaturedImg,
                    FeaturedImgURL = addCaseRequest.FeaturedImgURL,
                    UrlHandle = UrlHandleMaker,
                    PublishedDate = addCaseRequest.PublishedDate,
                    Visible = addCaseRequest.Visible,
                    Practices = selectedPractices
                };

                if(selectedPractices.Count == 0 || UrlHandleMaker == null)
                {
                    // Re-populate Practices in case of a validation error
                    var practices = await practiceRepository.GetAllPracticeAsync();
                    addCaseRequest.Practices = practices.Select(x => new SelectListItem { Text = x.PracticeName, Value = x.Id.ToString() });

                    //For error handling and returning to AddCase without removing the existing input
                    ModelState.AddModelError("", "Please fill out this form.");
                    return View("AddCase", addCaseRequest); // Return to the form with validation error
                }
                else
                {
                    //Call repository to save to DB
                    await caseRepository.AddCaseAsync(casePost);

                    return RedirectToAction("ViewAllCase");
                }
            }
                        
        }

        private string GenerateUrlHandle(string input)
        {
            return input.Replace(" ", "-").ToLower();
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
            if (editCaseRequest.CaseName == null || editCaseRequest.Heading == null || editCaseRequest.ShortDescription == null || editCaseRequest.FeaturedImg == null || editCaseRequest.Content == null)
            {
                // Re-populate Practices in case of a validation error
                var practices = await practiceRepository.GetAllPracticeAsync();
                editCaseRequest.Practices = practices.Select(x => new SelectListItem { Text = x.PracticeName, Value = x.Id.ToString() });

                //For error handling and returning to AddCase without removing the existing input
                ModelState.AddModelError("", "Please fill out this form.");
                return View("EditCase", editCaseRequest); // Return to the form with validation error
            }
            else
            {
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

                var UrlHandleMaker = GenerateUrlHandle(editCaseRequest.Heading);

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
                    UrlHandle = UrlHandleMaker,
                    PublishedDate = editCaseRequest.PublishedDate,
                    Visible = editCaseRequest.Visible,
                    Practices = selectedPractices
                };

                //casePostModel.Practices = selectedPractices;
                if (selectedPractices.Count == 0 || UrlHandleMaker == null)
                {
                    // Re-populate Practices in case of a validation error
                    var practices = await practiceRepository.GetAllPracticeAsync();
                    editCaseRequest.Practices = practices.Select(x => new SelectListItem { Text = x.PracticeName, Value = x.Id.ToString() });

                    //For error handling and returning to AddCase without removing the existing input
                    ModelState.AddModelError("", "Please choose at least one practice area.");
                    return View("EditCase", editCaseRequest); // Return to the form with validation error
                }
                else
                {
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
