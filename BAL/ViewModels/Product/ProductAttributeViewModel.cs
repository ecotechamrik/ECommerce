using BAL.ViewModels.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BAL.ViewModels.Product
{
    public class ProductAttributeViewModel
    {
        public int ProductAttributeID { get; set; }

        [Display(Name = "Door Type Name")]
        [Required(ErrorMessage = "Enter Door Name")]
        public string ProductAttributeName { get; set; }
        public string Description { get; set; }
        public string ProductActiveAttributes { get; set; }
        public int? ProductID { get; set; }

        [Display(Name = "Currency Name")]
        [Required(ErrorMessage = "Select Currency")]
        public int? CurrencyID { get; set; }

        [Display(Name = "Currency Name")]
        public string CurrencyName { get; set; }
        public int? DoorTypeID { get; set; }

        [Display(Name = "Door Type Name")]
        public string DoorTypeName { get; set; }

        [Display(Name = "Basic Cost")]
        public double InboundCostDisplay { get; set; }
        public double InboundCost { get; set; }

        [Display(Name = "Transportation Cost")]
        public double TransportationCostDisplay { get; set; }
        public double TransportationCost { get; set; }

        [Display(Name = "Landed Cost")]
        public double LandedCostDisplay { get; set; }
        public double LandedCost { get; set; }

        public string ProductThicknessName { get; set; }        
        public IList<CurrencyViewModel> CurrencyList { get; set; }
        public IList<SupplierViewModel> SupplierList { get; set; }
        public IList<ProductThicknessViewModel> ProductThicknessList { get; set; }
    }
}