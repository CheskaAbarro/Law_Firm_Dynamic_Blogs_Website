namespace AbarroLaw.Web.Models.ViewModels
{
    public class GetPracticeandCaseRequest
    {
        public string PracticeName { get; set; }

        public string PracticeImageUrl { get; set; }

        public List<AbarroLaw.Web.Models.Domain.CasePost> CasePosts { get; set; }
    }
}
