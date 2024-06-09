namespace AbarroLaw.Web.Models.Domain
{
    public class EmailMessage
    {
        public Guid Id { get; set; }

        public string SenderName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string MessageTitle { get; set; }

        public string SenderMessage { get; set; }

        public DateTime DateSent { get; set; }


    }
}
