using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace BAL.Entities
{
    public class ProductAttribute
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductAttributeID { get; set; }        
        public int? ProductID { get; set; }
        public string Width { get; set; }
        public int? DoorTypeID { get; set; }


        /* -- Navigation Properties -- */
        public Product Product { get; set; }
        public DoorType DoorType { get; set; }
        public ICollection<ProductSizeAndPrice> ProductSizeAndPrices { get; set; }
    }
}