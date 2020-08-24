using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BAL.ViewModels.Product
{
    public class ProductSizeAndPriceViewModel
    {
        public int ProductSizeAndPriceID { get; set; }

        [Display(Name = "Product Code")]
        [Required(ErrorMessage ="Enter Product Code")]
        public string ProductCode { get; set; }
        public int? ProductAttributeID { get; set; }
        public int? ProductAttributeThicknessID { get; set; }

        [Display(Name = "Height")]
        [Required(ErrorMessage = "Select Product Height")]
        public int? ProductHeightID { get; set; }

        [Display(Name = "Height")]        
        public string ProductHeightName { get; set; }

        [Display(Name = "Currency")]
        [Required(ErrorMessage = "Enter Currency")]
        public int? CurrencyID { get; set; }

        [Display(Name = "Currency")]
        public string CurrencyName { get; set; }

        [Display(Name = "Price Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime PriceDate { get; set; }
        
        [Display(Name = "Inventory Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime InvDate { get; set; }
        
        [Display(Name = "RtlDis")]
        public double RetailPriceDisc { get; set; }

        [Display(Name = "PVoid")]
        public double PriceVoid { get; set; }
        
        [Display(Name = "Markup%")]
        public double Markup { get; set; }

        [Required(ErrorMessage = "Enter Selling Price")]
        [Display(Name = "SellingP")]
        public double SellingPrice { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime UpdatedDateTime { get; set; }

        /* -- Supplier Details -- */
        [Display(Name = "Supplier")]
        [Required(ErrorMessage = "Select Supplier")]
        public int? SupplierID { get; set; }

        [Display(Name = "Supplier")]
        [Required(ErrorMessage = "Select Supplier")]
        public string SupplierName { get; set; }

        [Display(Name = "Basic Cost")]
        public double InboundCostDisplay { get; set; }
        public double InboundCost { get; set; }

        [Display(Name = "Transport Cost")]
        public double TransportationCostDisplay { get; set; }
        public double TransportationCost { get; set; }

        [Display(Name = "Landed Cost")]
        public double LandedCostDisplay { get; set; }
        
        [Display(Name = "L-Cost")]
        public double LandedCost { get; set; }
        
        /* -- More Product Size Count -- */
        public int? MoreProductSizes { get; set; }

        [Display(Name = "Live")]
        public string Live { get; set; }

        [Display(Name = "Options")]
        public string Options { get; set; }
        public string ThicknessWidth { get; set; }
        public IList<SupplierViewModel> SupplierList { get; set; }
    }
}
