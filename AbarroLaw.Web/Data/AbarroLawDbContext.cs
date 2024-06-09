using AbarroLaw.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace AbarroLaw.Web.Data
{
    public class AbarroLawDbContext : DbContext
    {
        public AbarroLawDbContext(DbContextOptions<AbarroLawDbContext> options) : base(options)
        {

        }

        public DbSet<Practice> Practices { get; set; }

        public DbSet<CasePost> CasePosts { get; set; }

        public DbSet<EmailMessage> Messages { get; set; }
    }
}
