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

        [Display(Name = "Product Code", Prompt = "Product Code")]
        public string ProductCode { get; set; }

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

        [Display(Name = "Supplier ID", Prompt = "Supplier ID")]
        public int? SupplierID { get; set; }

        [Display(Name = "Supplier Mode Code", Prompt = "Supplier Mode Code")]
        public string SupplierModeCode { get; set; }

        [Display(Name = "Lead Time", Prompt = "Lead Time")]
        public string LeadTime { get; set; }

        [Display(Name = "Retail Bin", Prompt = "Retail Bin")]
        public string RetailBin { get; set; }

        [Display(Name = "Whole Sale Bin", Prompt = "Whole Sale Bin")]
        public string WholeSaleBin { get; set; }

        [Display(Name = "Index Number", Prompt = "Index Number")]
        public string IndexNumber { get; set; }

        [Display(Name = "Practical Markup", Prompt = "Practical Markup")]
        public string PracticalMarkup { get; set; }

        [Display(Name = "Practical Cost", Prompt = "Practical Cost")]
        public string PracticalCost { get; set; }

        [Display(Name = "Retail Markup Disc", Prompt = "Retail Markup Disc")]
        public string RetailMarkupDisc { get; set; }

        [Display(Name = "Retail Markup", Prompt = "Retail Markup")]
        public string RetailMarkup { get; set; }

        [Display(Name = "Live Price Disc", Prompt = "Live Price Disc")]
        public string LivePriceDisc { get; set; }

        [Display(Name = "Live Price", Prompt = "Live Price")]
        public string LivePrice { get; set; }

        [Display(Name = "Width", Prompt = "Width")]
        public string Width { get; set; }

        [Display(Name = "Category ID")]
        public int? CategoryID { get; set; }

        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }

        [Display(Name = "Sub Category ID", Prompt = "Sub Category ID")]
        public int? SubCategoryID { get; set; }

        [Display(Name = "Sub Category Name")]
        public string SubCategoryName { get; set; }

        [Display(Name = "IP Address", Prompt = "IP Address")]
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
        public int? CreatedBy { get; set; }
        
    }
}
