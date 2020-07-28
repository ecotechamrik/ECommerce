using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BAL.Entities
{
    public class ProductPrice
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductPriceID { get; set; }
        public int? ProductAttributeID { get; set; }
        public double CostingPrice { get; set; }
        public double MarkupPrice { get; set; }
        public int? CurrencyID { get; set; }
        public Currency Currency { get; set; }
        public ProductAttributes ProductAttributes { get; set; }
    }
}
