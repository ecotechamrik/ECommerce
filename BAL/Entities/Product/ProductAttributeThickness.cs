using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace BAL.Entities
{
    public class ProductAttributeThickness
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductAttributeThicknessID { get; set; }
        public int? ProductAttributeID { get; set; }
        public int? ProductThicknessID { get; set; }
        public string ProductCodeInitials { get; set; }
        public bool Active { get; set; }

        /* -- Navigation Properties -- */
        public ProductAttribute ProductAttribute { get; set; }
        public ProductThickness ProductThickness { get; set; }
        public ICollection<ProductSizeAndPrice> ProductSizeAndPrices { get; set; }
    }
}