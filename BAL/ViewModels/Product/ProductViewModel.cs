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

        //[Display(Name = "Product Code", Prompt = "Product Code")]
        //public string ProductCode { get; set; }

        [Display(Name = "Product Description", Prompt = "Product Description")]
        public string ProductDesc { get; set; }

        [Display(Name = "Priority Number", Prompt = "Priority Number")]
        public string PriorityNumber { get; set; }

        [Display(Name = "Notes", Prompt = "Notes")]
        public string Notes { get; set; }

        [Display(Name = "Group Number", Prompt = "Group Number")]
        public string GroupNumber { get; set; }

        [Display(Name = "Building Code", Prompt = "Building Code")]
        public string BuildingCode { get; set; }

        [Display(Name = "Location Code", Prompt = "Location Code")]
        public string LocationCode { get; set; }

        [Display(Name = "Retail Bin", Prompt = "Retail Bin")]
        public string RetailBin { get; set; }

        [Display(Name = "Whole Sale Bin", Prompt = "Whole Sale Bin")]
        public string WholeSaleBin { get; set; }

        [Display(Name = "Index Number", Prompt = "Index Number")]
        public string IndexNumber { get; set; }

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

        [Display(Name = "Best Quantity")]
        public int BestQuantity { get; set; }

        [Display(Name = "Current Quantity")]
        public int CurrentQuantity { get; set; }

        [Display(Name = "IP Address")]
        public string IPAddress { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; }

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