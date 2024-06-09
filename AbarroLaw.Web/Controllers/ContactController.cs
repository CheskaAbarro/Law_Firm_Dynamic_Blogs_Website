using AbarroLaw.Web.Models.Domain;
using AbarroLaw.Web.Models.ViewModels;
using AbarroLaw.Web.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AbarroLaw.Web.Controllers
{
    public class ContactController : Controller
    {
        private readonly IMessageRepository messageRepository;

        public ContactController(IMessageRepository messageRepository)
        {
            this.messageRepository = messageRepository;
        }


        public IActionResult ContactUs()
        {
            return View();
        }

        //Add - Store message inquiry in database --------------------------
        [HttpPost]
        public async Task<IActionResult> SendMessage(AddEmailRequest addEmailRequest)
        {
            var emailModel = new EmailMessage
            {
                SenderMessage = addEmailRequest.SenderMessage,
                Email = addEmailRequest.Email,
                Phone = addEmailRequest.Phone,
                MessageTitle = addEmailRequest.MessageTitle,
                SenderName = addEmailRequest.SenderName,
                DateSent = addEmailRequest.DateSent
            };

            await messageRepository.SendMessageAsync(emailModel);

            return RedirectToAction("ContactUs");
        }

        //View of emails for Admin
        public async Task<IActionResult> ViewAllEmails()
        {
            var allMessages = await messageRepository.GetAllMessageAsync();

            return View(allMessages);
        }

        //View individual inquiry
        public async Task<IActionResult> ViewInquiry(Guid id)
        {
            var message = await messageRepository.GetMessageAsync(id);

            if (message != null)
            {
                var viewMessage = new EmailMessage
                {
                    Id = message.Id,
                    SenderName = message.SenderName,
                    Email = message.Email,
                    Phone = message.Phone,
                    MessageTitle = message.MessageTitle,
                    SenderMessage = message.SenderMessage,
                    DateSent = message.DateSent
                };

                return View(viewMessage);
            }
            else
            {
                return View(null);
            }

        }
    }
}
