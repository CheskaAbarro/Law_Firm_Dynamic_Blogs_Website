namespace AbarroLaw.Web.Repositories
{
    public interface IImageRepository
    {
        Task<string> UploadImageAsync(IFormFile file);
    }
}
