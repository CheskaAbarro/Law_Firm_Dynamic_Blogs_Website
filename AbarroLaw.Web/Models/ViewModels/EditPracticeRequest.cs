using System.ComponentModel.DataAnnotations;

namespace AbarroLaw.Web.Models.ViewModels
{
    public class EditPracticeRequest
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Please fill out practice name")]
        public string PracticeName { get; set; }

        [Required(ErrorMessage = "Please give practice description")]
        public string PracticeDescription { get; set; }

        [Required(ErrorMessage = "Please choose image for practice")]
        public string PracticeImage { get; set; }

        public string PracticeImageURL { get; set; }

        public string Visible { get; set; }
    }
}
