using System;
using System.Collections.Generic;

namespace BAL.Entities
{
    public class Product
    {
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductDesc { get; set; }
        public int? CategoryID { get; set; }
        public int? SubCategoryID { get; set; }
        public int? ProductDesignID { get; set; }
        public int? ProductGradeID { get; set; }
        public string IPAddress { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }

        // Navigation Property
        //[ForeignKey("CategoryID")] //-- CategoryID
        public Category Category { get; set; }
        public SubCategory SubCategory { get; set; }
        public ProductDesign ProductDesign { get; set; }
        public ProductGrade ProductGrade { get; set; }
        public ICollection<ProductAttribute> ProductAttributes { get; set; }
        public ICollection<ProductImage> ProductImages { get; set; }
    }
}