using Microsoft.AspNetCore.Mvc;

namespace AbarroLaw.Web.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult ContactUs()
        {
            return View();
        }
    }
}
