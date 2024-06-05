namespace AbarroLaw.Web.Models.Domain
{
    public class CasePost
    {
        public Guid Id { get; set; }

        public string CaseName { get; set; }

        public string Heading { get; set; }

        public string Content { get; set; }

        public string ShortDescription { get; set; }

        public string? FeaturedImg { get; set; }

        public string? FeaturedImgURL { get; set; }

        public DateTime PublishedDate { get; set; }

        public string Visible { get; set; }

        public ICollection<Practice> Practices { get; set; }
    }
}
