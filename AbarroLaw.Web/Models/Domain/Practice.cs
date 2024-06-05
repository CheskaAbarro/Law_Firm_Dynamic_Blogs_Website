namespace AbarroLaw.Web.Models.Domain
{
    public class Practice
    {
        public Guid Id { get; set; }

        public string PracticeName { get; set; }

        public string PracticeDescription { get; set; }

        public string PracticeImage { get; set; }

        public string PracticeImageURL { get; set; }

        public string Visible { get; set; }

        public ICollection<CasePost> CasePosts { get; set; }
    }
}
