using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BAL.ViewModels.Product
{
    public class SubCategoryViewModel
    {
        [Display(Name = "Sub Category ID")]
        public int SubCategoryID { get; set; }

        [Display(Name = "Sub Category Name", Prompt = "Sub Category Name")]
        [Required(ErrorMessage = "Enter Sub Category Name")]
        public string SubCategoryName { get; set; }
        public string SubCategoryCode { get; set; }

        [Display(Name = "Sub Category Description", Prompt = "Sub Category Description")]
        public string Description { get; set; }

        [Display(Name = "Category ID")]
        public int? CategoryID { get; set; }

        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; }

        [JsonIgnore]
        public string Active
        {
            get { return IsActive == true ? "Yes" : "No"; }
        }
    }
}
