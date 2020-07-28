using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BAL.Entities
{
    public class ProductAttributes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductAttributeID { get; set; }

        public int? ProductID { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }
        public bool IsActive { get; set; }
        public int? DoorTypeID { get; set; }        
        public Product Product { get; set; }
        public DoorType DoorType { get; set; }
        public ICollection<ProductPrice> ProductPrice { get; set; }
    }
}
