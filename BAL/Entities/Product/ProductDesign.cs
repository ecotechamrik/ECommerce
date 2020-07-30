using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BAL.Entities
{
    public class ProductDesign
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductDesignID { get; set; }
        public string ProductDesignName { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
