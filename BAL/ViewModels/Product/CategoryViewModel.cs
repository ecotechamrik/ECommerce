using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json.Serialization;

namespace BAL.ViewModels.Product
{
    public class CategoryViewModel
    {
        [Display(Name = "Category ID")]
        public int CategoryID { get; set; }

        [Display(Name = "Category Name", Prompt = "Category Name")]
        [Required(ErrorMessage = "Enter Category Name")]
        public string CategoryName { get; set; }

        [Display(Name = "Category Name")]
        public string DisplayCategoryName
        {
            get
            {
                if (CategoryName.Length <= 50)
                    return CategoryName;
                else
                    return CategoryName.Substring(0, 50) + "...";
            }
        }

        [Display(Name = "Category Description", Prompt = "Category Description")]
        public string Description { get; set; }

        [Display(Name = "Category Order", Prompt = "Category Order")]
        public int CategoryOrder { get; set; }

        [Display(Name = "Section ID")]
        [Required(ErrorMessage = "Select Section")]
        public int? SectionID { get; set; }

        [Display(Name = "SectionName")]
        public string SectionName { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; }

        [JsonIgnore]
        public string Active
        {
            get { return IsActive == true ? "Yes" : "No"; }
        }
    }
}
