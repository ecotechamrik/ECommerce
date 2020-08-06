using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BAL.ViewModels.Product
{
    public class SupplierViewModel
    {
        public int SupplierID { get; set; }

        [Required(ErrorMessage = "Please Enter Supplier Name")]
        [Display(Name = "Supplier Name", Prompt = "Supplier Name")]
        public string Name { get; set; }

        [Display(Name = "Model Code")]
        public string ModelCode { get; set; }

        [Display(Name = "Inbound Cost")]
        public double InboundCost { get; set; }

        [Display(Name = "Landed Cost")]
        public double LandedCost { get; set; }

        [Display(Name = "Supplier Cost")]
        public double SupplierCost { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime UpdatedDateTime { get; set; }
    }
}
