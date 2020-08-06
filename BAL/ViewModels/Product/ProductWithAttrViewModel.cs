using System;
using System.ComponentModel.DataAnnotations;

namespace BAL.ViewModels.Product
{
    public class ProductWithAttrViewModel: ProductViewModel
    {
        public int ProductAttributeID { get; set; }

        [Display(Name = "Product Code")]
        public string ProductCode { get; set; }

        /* -- Size Attributes -- */
        public new int? ProductID { get; set; }
        
        [Display(Name = "Width")]
        [Required(ErrorMessage = "Enter Width")]
        public string Width { get; set; }

        [Display(Name = "Height 80\"")]
        public string Height80 { get; set; }

        [Display(Name = "Height 84\"")]
        public string Height84 { get; set; }

        [Display(Name = "Height 90\"")]
        public string Height90 { get; set; }

        [Display(Name = "Height 96\"")]
        public string Height96 { get; set; }
        public new bool IsActive { get; set; }
        public int? DoorTypeID { get; set; }


        /* -- Price Columnns -- */
        [Display(Name = "Price Void")]
        public double PriceVoid { get; set; }

        [Display(Name = "Price Date")]
        public DateTime PriceDate { get; set; }

        [Display(Name = "Inventory Date")]
        public DateTime InvDate { get; set; }
        public double Markup { get; set; }

        [Display(Name = "Retail Price Discount")]
        public double RetailPriceDisc { get; set; }
        public int? CurrencyID { get; set; }
    }
}
