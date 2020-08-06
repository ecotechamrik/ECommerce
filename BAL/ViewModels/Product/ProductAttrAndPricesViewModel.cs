using System;
using System.ComponentModel.DataAnnotations;

namespace BAL.ViewModels.Product
{
    public class ProductSizeAndPriceViewModel
    {
        public int ProductAttributeID { get; set; }

        [Display(Name = "Product Code", Prompt = "Product Code")]
        public string ProductCode { get; set; }

        /* -- Size Attributes -- */
        public int? ProductID { get; set; }
        
        [Display(Name = "Width", Prompt = "Width")]
        [Required(ErrorMessage = "Enter Width")]
        public string Width { get; set; }

        [Display(Name = "Height 80\"", Prompt = "Enter 80\" Price")]
        public string Height80 { get; set; }

        [Display(Name = "Height 84\"", Prompt = "Enter 84\" Price")]
        public string Height84 { get; set; }

        [Display(Name = "Height 90\"", Prompt = "Enter 90\" Price")]
        public string Height90 { get; set; }

        [Display(Name = "Height 96\"", Prompt = "Enter 96\" Price")]
        public string Height96 { get; set; }
        public bool IsActive { get; set; }
        public int? DoorTypeID { get; set; }


        /* -- Price Columnns -- */
        [Display(Name = "Price Void", Prompt = "Price Void")]
        public double PriceVoid { get; set; }

        [Display(Name = "Price Date")]
        public DateTime PriceDate { get; set; }

        [Display(Name = "Inventory Date")]
        public DateTime InvDate { get; set; }
        public double Markup { get; set; }

        [Display(Name = "Retail Price Discount", Prompt = "Retail Price Discount")]
        public double RetailPriceDisc { get; set; }
        public int? CurrencyID { get; set; }
    }
}
