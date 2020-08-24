using Newtonsoft.Json;
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
        public string SupplierName { get; set; }

        [Display(Name = "Supplier Code")]
        public string SupplierCode { get; set; }

        [Display(Name = "Basic Cost")]
        public double InboundCost { get; set; } = 0;

        [Display(Name = "Transportation Cost")]
        public double TransportationCost { get; set; } = 0;

        [Display(Name = "Landed Cost")]
        public double LandedCost { get; set; } = 0;
        public DateTime CreatedDateTime { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public bool IsActive { get; set; }
        [JsonIgnore]
        public string Active
        {
            get { return IsActive == true ? "Yes" : "No"; }
        }
    }
}
