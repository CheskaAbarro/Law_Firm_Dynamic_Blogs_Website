using AbarroLaw.Web.Models.Domain;

namespace AbarroLaw.Web.Repositories
{
    public interface ICaseRepository
    {
        //Get all practice for practice viewing (View all practice View)
        Task<IEnumerable<CasePost>> GetAllCaseAsync();

        //Get All Practice for Practice Area View (for user view)
        Task<List<CasePost>> GetAllCaseAsyncForUser();

        //Get Single Practice for Edit
        Task<CasePost?> GetCaseAsync(Guid id);

        //Add
        Task<CasePost> AddCaseAsync(CasePost casePost);

        //Update
        Task<CasePost?> UpdateCaseAsync(CasePost casePost);

        //Delete
        Task<CasePost?> DeleteCaseAsync(Guid id);
    }
}
