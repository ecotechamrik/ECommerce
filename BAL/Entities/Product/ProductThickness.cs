using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BAL.Entities
{
    public class ProductThickness
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductThicknessID { get; set; }
        public string ProductThicknessName { get; set; }
        public ICollection<ProductAttributeThickness> ProductAttributeThicknesses { get; set; }
    }
}
