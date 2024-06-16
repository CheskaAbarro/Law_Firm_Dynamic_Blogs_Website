using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace AbarroLaw.Web.Models.ViewModels
{
    public class AddEmailRequest
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage ="Please enter your name.")]
        public string SenderName { get; set; }

        [Required(ErrorMessage = "Please enter email.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter phone.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Please give message title.")]
        public string MessageTitle { get; set; }

        [Required(ErrorMessage = "Please enter your message.")]
        public string SenderMessage { get; set; }


        public DateTime DateSent { get; set; }

    }
}
