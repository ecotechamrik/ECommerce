using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BAL.Entities
{
    public class ProductWidth
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductWidthID { get; set; }
        public string ProductWidthName { get; set; }
        public ICollection<ProductSizeAndPrice> ProductSizeAndPrices { get; set; }
    }
}
