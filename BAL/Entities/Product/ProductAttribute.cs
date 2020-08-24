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
        public int? DoorTypeID { get; set; }        
        public int? CurrencyID { get; set; }
        public int? SupplierID { get; set; }


        /* -- Navigation Properties -- */
        public Product Product { get; set; }
        public DoorType DoorType { get; set; }
        public Currency Currency { get; set; }
        public Supplier Supplier { get; set; }
        public ICollection<ProductAttributeThickness> ProductAttributeThicknesses { get; set; }
    }
}