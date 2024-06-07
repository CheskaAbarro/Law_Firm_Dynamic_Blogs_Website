using AbarroLaw.Web.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AbarroLaw.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }


        [HttpPost("upload-image")]
        public async Task<IActionResult> UploadImageAsync(IFormFile file)
        {
            
            var imageURL = await imageRepository.UploadImageAsync(file);
            if (imageURL == null)
            {
                return Problem("Something went wrong! ", null, (int)HttpStatusCode.InternalServerError);
            }
            else
            {
                return new JsonResult(new { link = imageURL });
            }
        }

        [HttpPost("upload-editor-image")]
        public async Task<IActionResult> UploadImageEditorAsync(IFormFile file)
        {

            var imageURL = await imageRepository.UploadImageAsync(file);
            if (imageURL == null)
            {
                return Problem("Something went wrong! ", null, (int)HttpStatusCode.InternalServerError);
            }
            else
            {
                var correctImageUrl = Url.Content("~/images/uploads/") + Path.GetFileName(imageURL);
                return new JsonResult(new { link = correctImageUrl });
            }
        }
    }
}
