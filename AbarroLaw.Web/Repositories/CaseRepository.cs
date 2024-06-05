using AbarroLaw.Web.Data;
using AbarroLaw.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace AbarroLaw.Web.Repositories
{
    public class CaseRepository : ICaseRepository
    {
        private readonly AbarroLawDbContext abarroLawDbContext;

        public CaseRepository(AbarroLawDbContext abarroLawDbContext)
        {
            this.abarroLawDbContext = abarroLawDbContext;
        }

        //Add -------------
        public async Task<CasePost> AddCaseAsync(CasePost casePost)
        {
            await abarroLawDbContext.AddAsync(casePost);
            await abarroLawDbContext.SaveChangesAsync();
            return casePost;
        }

        //Get -------------
        //Get All Practice for Practice Area View (for user)
        public async Task<IEnumerable<CasePost>> GetAllCaseAsync()
        {
            return await abarroLawDbContext.CasePosts.Include(x => x.Practices).ToListAsync();
        }

        //Get cases based on practice tag
        public async Task<IEnumerable<CasePost>> GetCasePostsByPracticeNameAsync(string practiceName)
        {
            var practiceCasePost = await abarroLawDbContext.CasePosts
                                .Include(x => x.Practices)
                                .Where(cp => cp.Practices.Any(p => p.PracticeName == practiceName))
                                .ToListAsync();

            return practiceCasePost;
        }

        //Get Single Practice for Edit
        public async Task<CasePost?> GetCaseAsync(Guid id)
        {
           return await abarroLawDbContext.CasePosts
                .Include(x => x.Practices)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        //Edit -------------
        public async Task<CasePost?> UpdateCaseAsync(CasePost casePost)
        {
            var existingCase = await abarroLawDbContext.CasePosts.Include(x => x.Practices).FirstOrDefaultAsync(x => x.Id == casePost.Id);

            if(existingCase != null)
            {
                existingCase.Id = casePost.Id;
                existingCase.CaseName = casePost.CaseName;
                existingCase.Heading = casePost.Heading;
                existingCase.Content = casePost.Content;
                existingCase.ShortDescription = casePost.ShortDescription;
                existingCase.FeaturedImg = casePost.FeaturedImg;
                existingCase.FeaturedImgURL = casePost.FeaturedImgURL;
                existingCase.PublishedDate = casePost.PublishedDate;
                existingCase.Visible = casePost.Visible;
                existingCase.Practices = casePost.Practices;

                await abarroLawDbContext.SaveChangesAsync();

                return existingCase;
            }
            else
            {
                return null;
            }
        }

        //Delete -----------
        public async Task<CasePost?> DeleteCaseAsync(Guid id)
        {
            var existingCase = await abarroLawDbContext.CasePosts.FindAsync(id);

            if(existingCase != null)
            {
                abarroLawDbContext.CasePosts.Remove(existingCase);
                abarroLawDbContext.SaveChanges();
                return existingCase;
            }
            else
            {
                return null;
            }
        }

        
    }
}
