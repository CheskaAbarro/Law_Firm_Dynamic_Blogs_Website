namespace AbarroLaw.Web.Models.ViewModels
{
    public class EditPracticeRequest
    {
        public Guid Id { get; set; }

        public string PracticeName { get; set; }

        public string PracticeDescription { get; set; }

        public string PracticeImage { get; set; }

        public string PracticeImageURL { get; set; }

        public string Visible { get; set; }
    }
}
