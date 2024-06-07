using AbarroLaw.Web.Models.Domain;
using AbarroLaw.Web.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AbarroLaw.Web.Controllers
{
    public class DynamicCasePostController : Controller
    {
        private readonly ICaseRepository caseRepository;

        public DynamicCasePostController(ICaseRepository caseRepository)
        {
            this.caseRepository = caseRepository;
        }


        public async Task<IActionResult> CasePostAsync(string urlHandle)
        {

            var casePosts = await caseRepository.GetUrlHandle(urlHandle);
            var casePostsDetails = new CasePost();

            if (casePosts != null)
            {
                casePostsDetails = new CasePost
                {
                    Id = casePosts.Id,
                    CaseName = casePosts.CaseName,
                    Heading = casePosts.Heading,
                    Content = casePosts.Content,
                    ShortDescription = casePosts.ShortDescription,
                    FeaturedImg = casePosts.FeaturedImg,
                    FeaturedImgURL = casePosts.FeaturedImgURL,
                    UrlHandle = casePosts.UrlHandle,
                    PublishedDate = casePosts.PublishedDate,
                    Visible = casePosts.Visible,
                    Practices = casePosts.Practices

                };

                return View(casePostsDetails);
            }
            else
            {
                return View();
            }

        }
    }
}
