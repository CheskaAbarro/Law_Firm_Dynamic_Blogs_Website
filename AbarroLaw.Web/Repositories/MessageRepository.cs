using AbarroLaw.Web.Data;
using AbarroLaw.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace AbarroLaw.Web.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly AbarroLawDbContext abarroLawDbContext;

        public MessageRepository(AbarroLawDbContext abarroLawDbContext)
        {
            this.abarroLawDbContext = abarroLawDbContext;
        }

        //Get all message view
        public async Task<List<EmailMessage>> GetAllMessageAsync()
        {
            return await abarroLawDbContext.Messages.ToListAsync();
        }

        //Get single message view
        public async Task<EmailMessage?> GetMessageAsync(Guid id)
        {
            return await abarroLawDbContext.Messages.FirstOrDefaultAsync(x => x.Id == id);
        }

        //Store to database 
        public async Task<EmailMessage> SendMessageAsync(EmailMessage emailMessage)
        {
            await abarroLawDbContext.AddAsync(emailMessage);
            await abarroLawDbContext.SaveChangesAsync();
            return emailMessage;
        }
    }
}
