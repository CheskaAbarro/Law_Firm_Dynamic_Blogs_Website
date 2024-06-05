using AbarroLaw.Web.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AbarroLaw.Web.Controllers
{
    public class PracticeAreaController : Controller
    {
        private readonly IPracticeRepository practiceRepository;
        private readonly ICaseRepository caseRepository;

        public PracticeAreaController(IPracticeRepository practiceRepository, ICaseRepository caseRepository)
        {
            this.practiceRepository = practiceRepository;
            this.caseRepository = caseRepository;
        }


        //Practice Area Main ----------------------------------------
        [HttpGet]
        public async Task<IActionResult> PracticeAreaView()
        {
            var otherPractices = await practiceRepository.GetAllPracticesAsyncForUser();
            if (otherPractices != null && otherPractices.Any())
            {
                return View(otherPractices);
            }
            else
            {
                return View();
            }

        }

        //Practice Area Dynamic (based on DB) -----------------------
        public async Task<IActionResult> PracticeAreaDynamic()
        {
            var casePosts = await caseRepository.GetAllCaseAsync();

            return View(casePosts);
        }


        
    }
}
