using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BAL.Entities
{
    public class ProductImage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductImageID { get; set; }
        public string ThumbNailSizeImage { get; set; }
        public string OriginalImage { get; set; }
        public int Order { get; set; }
        public bool IsMainImage { get; set; }
        public int? ProductID { get; set; }
        public Product Product { get; set; }

    }
}
