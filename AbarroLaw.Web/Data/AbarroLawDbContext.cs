using AbarroLaw.Web.Models.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AbarroLaw.Web.Data
{
    public class AbarroLawDbContext : DbContext
    {
        public AbarroLawDbContext(DbContextOptions<AbarroLawDbContext> options) : base(options)
        {

        }

        //Databases for Practice, Case Posts and Emails
        public DbSet<Practice> Practices { get; set; }

        public DbSet<CasePost> CasePosts { get; set; }

        public DbSet<EmailMessage> Messages { get; set; }

    }
}
