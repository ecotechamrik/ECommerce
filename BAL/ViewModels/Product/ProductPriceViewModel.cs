using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BAL.ViewModels.Product
{
    public class ProductPriceViewModel
    {
        public int ProductPriceID { get; set; }
        public int? ProductAttributeID { get; set; }

        // Original cost of the product
        [DisplayName("Costing Price")]
        public double CostingPrice { get; set; }

        // Set the Markup Price to Set the Retail Price
        [DisplayName("Markup Price")]
        public double MarkupPrice { get; set; }
        public int? CurrencyID { get; set; }
    }
}
