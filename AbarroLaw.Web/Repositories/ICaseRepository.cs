using AbarroLaw.Web.Models.Domain;

namespace AbarroLaw.Web.Repositories
{
    public interface ICaseRepository
    {
        //Get all practice for practice viewing (View all practice View)
        Task<IEnumerable<CasePost>> GetAllCaseAsync();

        //Get cases based on practice tag
        Task<IEnumerable<CasePost>> GetCasePostsByPracticeNameAsync(string practiceName);

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
