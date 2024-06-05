using AbarroLaw.Web.Models.Domain;
using Azure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AbarroLaw.Web.Repositories
{
    public interface IPracticeRepository
    {

        //Get all practice for practice viewing (View all practice View)
        Task<IEnumerable<Practice>> GetAllPracticeAsync();

        //Get All Practice for Practice Area View (for user view)
        Task<List<Practice>> GetAllPracticesAsyncForUser();

        //Get Single Practice for Edit
        Task<Practice?> GetPracticeAsync(Guid id);

        //Add
        Task<Practice> AddPracticeAsync(Practice practice);

        //Update
        Task<Practice?> UpdatePracticeAsync(Practice practice);

        //Delete
        Task<Practice?> DeletePracticeAsync(Guid id);

    }
}