using BAL.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace BAL.ViewModels.Product
{
    public class ProductSizeAndPriceViewModel
    {
        public int ProductSizeAndPriceID { get; set; }

        [Display(Name = "Product Code")]
        [Required(ErrorMessage = "Enter Product Code")]
        public string ProductCode { get; set; }
        public string Description { get; set; }
        public int? ProductAttributeThicknessID { get; set; }

        [Display(Name = "Thickness")]
        public string ProductThicknessName { get; set; }
        public int? ProductHeightID { get; set; }

        [Display(Name = "Height")]
        public string ProductHeightName { get; set; }

        [Display(Name = "Width")]
        [Required(ErrorMessage = "Select Product Width")]
        public int? ProductWidthID { get; set; }

        private DateTime _priceDate;

        [Display(Name = "Price Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime? PriceDate
        {
            get
            {
                if (_priceDate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture) == "0001/01/01")
                    return DateTime.Now;
                else
                    return _priceDate;
            }
            set { _priceDate = Convert.ToDateTime(value); }
        }

        private DateTime _invDate;

        [Display(Name = "Inventory Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime? InvDate
        {
            get
            {
                if (_invDate.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture) == "0001/01/01")
                    return DateTime.Now;
                else
                    return _invDate;
            }
            set { _invDate = Convert.ToDateTime(value); }
        }

        public DateTime? PriceVoidDate { get; set; }

        [Display(Name = "RtlDis%")]
        public double? RetailPriceDisc { get; set; }

        [Display(Name = "RtlPrc")]
        public double? RetailPrice { get; set; }

        [Display(Name = "PVoid")]
        public double? PriceVoid { get; set; }

        public DateTime? CreatedDateTime { get; set; }
        public DateTime? UpdatedDateTime { get; set; }

        [Display(Name = "Priority Number", Prompt = "Priority Number")]
        public double? PriorityNumber { get; set; }

        [Display(Name = "Inventory Number", Prompt = "Inventory Number")]
        public string InventoryNumber { get; set; }

        [Display(Name = "Notes", Prompt = "Notes")]
        public string Notes { get; set; }

        [Display(Name = "Group Number", Prompt = "Group Number")]
        public double? GroupNumber { get; set; }

        [Display(Name = "Building Code", Prompt = "Building Code")]
        public string BuildingCode { get; set; }

        [Display(Name = "Location Code", Prompt = "Location Code")]
        public string LocationCode { get; set; }

        [Display(Name = "Inventory Level")]
        public int? InventoryLevel { get; set; }

        [Display(Name = "Lead Time", Prompt = "Lead Time")]
        public string LeadTime { get; set; }

        [Display(Name = "Best Quantity No")]
        public int? BestQuantityNo { get; set; }

        [Display(Name = "Order Now No")]
        public int? OrderNowNo { get; set; }

        [Display(Name = "Retail Bin", Prompt = "Retail Bin")]
        public string RetailBin { get; set; }

        [Display(Name = "Whole Sale Bin", Prompt = "Whole Sale Bin")]
        public string WholeSaleBin { get; set; }

        [Display(Name = "Index Number", Prompt = "Index Number")]
        public int? IndexNumber { get; set; }

        public double? PracticalMarkup { get; set; }
        public double? PracticalCost { get; set; }


        [Display(Name = "Markup%")]
        public double? Markup { get; set; }

        public string MarkupDisplay
        {
            get
            {
                if (Markup != 0)
                    return String.Format("Markup%: {0}%",(Markup * 100) );
                else
                    return String.Format("Markup%: 0%");
            }
        }

        [Required(ErrorMessage = "Enter Selling Price")]
        [Display(Name = "SellingP")]
        public double? SellingPrice { get; set; }

        public double? RetailMarkupDisc { get; set; }

        public string RetailDiscDisplay
        {
            get
            {
                if (RetailPriceDisc != 0)
                    return String.Format("RtlDis%: {0}%", (RetailPriceDisc * 100));
                else
                    return String.Format("RtlDis%: 0%");
            }
        }
        public double? LivePriceDisc { get; set; }

        public string FormAction { get; set; }

        public string ThicknessIDWidthID { get; set; }
        public string ThicknessName { get; set; }
        public string WidthName { get; set; }

        [Display(Name = "80\"")]
        public string Column80 { get; set; }

        [Display(Name = "84\"")]
        public string Column84 { get; set; }

        [Display(Name = "90\"")]
        public string Column90 { get; set; }

        [Display(Name = "96\"")]
        public string Column96 { get; set; }

        [Display(Name = "108\"")]
        public string Column108 { get; set; }
        public IList<ProductSupplierViewModel> ProductSuppliers { get; set; }
    }
}
