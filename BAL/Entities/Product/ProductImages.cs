using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BAL.Entities
{
    public class ProductImages
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductImageID { get; set; }
        public string ProductImageName { get; set; }
        public int? ProductID { get; set; }
        public Product Product { get; set; }

    }
}
