using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BAL.ViewModels.Product
{
    public class ProductSupplierViewModel: SupplierViewModel
    {
        public int ProductSupplierID { get; set; }
        public int? ProductSizeAndPriceID { get; set; }

        [Display(Name = "Landed Cost")]
        public double LandedCostDisplay { get; set; }
        [Display(Name = "Basic Cost")]
        public double InboundCostDisplay { get; set; }

        [Display(Name = "Transport Cost")]
        public double TransportationCostDisplay { get; set; }

        [Display(Name = "L-Cost")]
        public new double LandedCost { get; set; }

        [Display(Name = "Options")]
        public string SupplierOption { get; set; }
        public bool IsOption { get; set; }

        [Display(Name = "Live")]
        public string Live { get; set; }
        public bool IsLive { get; set; }
        public string FormAction { get; set; }
        public string ThicknessIDHeightID { get; set; }
        public int ProductWidthID { get; set; }
    }
}