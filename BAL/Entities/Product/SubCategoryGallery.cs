using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BAL.Entities
{
    public class SubCatGallery
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SubCatGalleryID { get; set; }
        public string ThumbNailSizeImage { get; set; }
        public string MediumSizeImage { get; set; }
        public string LargeSizeImage { get; set; }
        public int Order { get; set; }
        public bool IsMainImage { get; set; }
        public int? CategoryID { get; set; }
        public int? SubCategoryID { get; set; }
        public Category Category { get; set; }
        public SubCategory SubCategory { get; set; }
    }
}