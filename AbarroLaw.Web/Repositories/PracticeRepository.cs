using AbarroLaw.Web.Data;
using AbarroLaw.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace AbarroLaw.Web.Repositories
{
    public class PracticeRepository : IPracticeRepository
    {
        private readonly AbarroLawDbContext abarroLawDbContext;

        public PracticeRepository(AbarroLawDbContext abarroLawDbContext)
        {
            this.abarroLawDbContext = abarroLawDbContext;
        }



        //Add ----------------
        public async Task<Practice> AddPracticeAsync(Practice practice)
        {
            await abarroLawDbContext.Practices.AddAsync(practice);
            await abarroLawDbContext.SaveChangesAsync();

            return practice;
        }



        //Get Practice ----------

        //Get all practice for practice viewing (View all practice View)
        public async Task<IEnumerable<Practice>> GetAllPracticeAsync()
        {
            return await abarroLawDbContext.Practices.ToListAsync();
        }

        //Get All Practice for Practice Area View (for user)
        public async Task<List<Practice>> GetAllPracticesAsyncForUser()
        {
            return await abarroLawDbContext.Practices.ToListAsync();
        }

        //Get Single Practice for Edit
        public async Task<Practice?> GetPracticeAsync(Guid id)
        {
            return await abarroLawDbContext.Practices.FirstOrDefaultAsync(x => x.Id == id);
        }



        //Update ------------
        public async Task<Practice?> UpdatePracticeAsync(Practice practice)
        {
            var existingPractice = await abarroLawDbContext.Practices.FindAsync(practice.Id);
            if (existingPractice != null)
            {
                existingPractice.PracticeName = practice.PracticeName;
                existingPractice.PracticeDescription = practice.PracticeDescription;
                existingPractice.PracticeImage = practice.PracticeImage;
                existingPractice.PracticeImageURL = practice.PracticeImageURL;
                existingPractice.Visible = practice.Visible;

                await abarroLawDbContext.SaveChangesAsync();

                return existingPractice;
            }
            else
            {
                return null;
            }

        }


        //Delete -------------
        public async Task<Practice?> DeletePracticeAsync(Guid id)
        {
            var existingPractice = await abarroLawDbContext.Practices.FindAsync(id);

            if (existingPractice != null)
            {
                abarroLawDbContext.Practices.Remove(existingPractice);
                await abarroLawDbContext.SaveChangesAsync();

                return existingPractice;
            }
            else
            {
                return null;
            }
        }

        
    }
}
