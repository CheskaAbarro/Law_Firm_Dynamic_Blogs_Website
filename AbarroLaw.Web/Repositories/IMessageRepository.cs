using AbarroLaw.Web.Models.Domain;

namespace AbarroLaw.Web.Repositories
{
    public interface IMessageRepository
    {
        //Store message to database
        Task<EmailMessage> SendMessageAsync(EmailMessage emailMessage);


        //Get all messages
        Task<List<EmailMessage>> GetAllMessageAsync();

        //Get Single messages for individual viewing
        Task<EmailMessage?> GetMessageAsync(Guid id);
    }
}
