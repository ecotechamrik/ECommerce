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
        public string ProductAttributeName { get; set; }
        public int? DoorTypeID { get; set; }
        public string Description { get; set; }
        public string ProductActiveAttributes { get; set; }
        public int? ProductID { get; set; }        
        public int? CurrencyID { get; set; }


        /* -- Navigation Properties -- */
        public DoorType DoorType { get; set; }
        public Product Product { get; set; }        
        public Currency Currency { get; set; }        
        public ICollection<ProductAttributeThickness> ProductAttributeThicknesses { get; set; }
    }
}