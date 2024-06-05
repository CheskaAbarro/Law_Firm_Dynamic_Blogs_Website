
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AbarroLaw.Web.Repositories
{
    public class FolderImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment webHostEnvironment;

        public FolderImageRepository(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
        }

        public async Task<string> UploadImageAsync(IFormFile file)
        {
            string? serverFolder = null;
            string? imageRelativePath = null;
            string folder = "images/uploads/";

            if (file == null || file.Length == 0)
            {
                return null;
            }

            try
            {
                var uniqueName = Guid.NewGuid().ToString() + "_" + file.FileName;
                var relativePath = Path.Combine(folder, uniqueName).Replace("\\", "/");
                serverFolder = Path.Combine(webHostEnvironment.WebRootPath, relativePath);

                using (var fileStream = new FileStream(serverFolder, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                return relativePath;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
