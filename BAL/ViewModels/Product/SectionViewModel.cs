using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BAL.ViewModels.Product
{
    public class SectionViewModel
    {
        [Display(Name = "Section ID")]
        public int SectionID { get; set; }

        [Display(Name = "Section Name", Prompt = "Section Name")]
        [Required(ErrorMessage = "Enter Section Name")]
        public string SectionName { get; set; }

        [Display(Name = "Section Description", Prompt = "Section Description")]
        public string Description { get; set; }

        [Display(Name = "Section Order", Prompt = "Section Order")]
        public int SectionOrder { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; }

        [JsonIgnore]
        public string Active
        {
            get { return IsActive == true ? "Yes" : "No"; }
        }
    }
}
