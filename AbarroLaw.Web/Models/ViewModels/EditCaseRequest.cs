using Microsoft.AspNetCore.Mvc.Rendering;

namespace AbarroLaw.Web.Models.ViewModels
{
    public class EditCaseRequest
    {
        public Guid Id { get; set; }

        public string CaseName { get; set; }

        public string Heading { get; set; }

        public string Content { get; set; }

        public string ShortDescription { get; set; }

        public string? FeaturedImg { get; set; }

        public string? FeaturedImgURL { get; set; }

        public string UrlHandle { get; set; }

        public DateTime PublishedDate { get; set; }

        public string Visible { get; set; }

        //Display all practice
        public IEnumerable<SelectListItem> Practices { get; set; }

        //Colect single
        public string SelectedPractice { get; set; }

        //Collect all practice
        public string[] SelectedPractices { get; set; } = Array.Empty<string>();
    }
}
