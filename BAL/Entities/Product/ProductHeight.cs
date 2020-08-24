using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BAL.Entities
{
    public class ProductHeight
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductHeightID { get; set; }
        public string ProductHeightName { get; set; }
        public ICollection<ProductSizeAndPrice> ProductSizeAndPrices { get; set; }
    }
}
