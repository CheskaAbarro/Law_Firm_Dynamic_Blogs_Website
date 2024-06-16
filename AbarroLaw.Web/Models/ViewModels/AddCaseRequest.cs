using AbarroLaw.Web.Models.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace AbarroLaw.Web.Models.ViewModels
{
    public class AddCaseRequest
    {

        [Required(ErrorMessage = "Please fill out case name")]
        public string CaseName { get; set; }

        [Required(ErrorMessage = "Please fill out heading")]
        public string Heading { get; set; }

        [Required(ErrorMessage = "Please give content for the case")]
        public string Content { get; set; }

        [Required(ErrorMessage = "Please give short description")]
        public string ShortDescription { get; set; }

        [Required(ErrorMessage = "Please choose image")]
        public string? FeaturedImg { get; set; }

        [Required]
        public string? FeaturedImgURL { get; set; }

        [Required]
        public string UrlHandle { get; set; }

        [Required]
        public DateTime PublishedDate { get; set; }

        public string Visible { get; set; }

        //Display all practice
        public IEnumerable<SelectListItem> Practices { get; set; }

        //Colect single
        public string SelectedPractice { get; set; }

        //Collect practice
        public string[] SelectedPractices { get; set; } = Array.Empty<string>();
    }
}
