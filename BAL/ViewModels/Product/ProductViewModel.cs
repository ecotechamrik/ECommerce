using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json.Serialization;

namespace BAL.ViewModels.Product
{
    public class ProductViewModel
    {
        [Display(Name = "ProductID ID")]
        public int ProductID { get; set; }

        [Display(Name = "Product Name", Prompt = "Product Name")]
        [Required(ErrorMessage = "Enter Product Name")]
        public string ProductName { get; set; }

        [Display(Name = "Product Name")]
        public string DisplayProductName
        {
            get
            {
                if (ProductName.Length <= 40)
                    return ProductName;
                else
                    return ProductName.Substring(0, 40) + "...";
            }
        }

        [Display(Name = "Product Description", Prompt = "Product Description")]
        public string ProductDesc { get; set; }

        [Display(Name = "Category ID")]
        public int? CategoryID { get; set; }

        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }

        [Display(Name = "Sub Category ID")]
        public int? SubCategoryID { get; set; }

        [Display(Name = "Sub Category Name")]
        public string SubCategoryName { get; set; }

        [Display(Name = "Product Design ID")]
        public int? ProductDesignID { get; set; }

        [Display(Name = "Product Design Name")]
        public string ProductDesignName { get; set; }

        [Display(Name = "Product Grade ID")]
        public int? ProductGradeID { get; set; }

        [Display(Name = "Product Grade Name")]
        public string ProductGradeName { get; set; }

        [Display(Name = "IP Address")]
        public string IPAddress { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; }

        [JsonIgnore]
        public string ProductCode { get; set; }

        [JsonIgnore]
        public string Active
        {
            get { return IsActive == true ? "Yes" : "No"; }
        }

        [Display(Name = "Created Date")]
        public DateTime? CreatedDate { get; set; }

        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }
        
    }
}