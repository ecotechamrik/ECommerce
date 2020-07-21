using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BAL.Entities
{
    public class SubCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SubCategoryID { get; set; }

        [Required]
        //[Column(TypeName = "nvarchar(100)")]
        public string SubCategoryName { get; set; }

        //[Column(TypeName = "nvarchar(500)")]
        public string Description { get; set; }

        public bool IsActive { get; set; }

        public int? CategoryID { get; set; }

        public Category Category { get; set; }

        // Get the Products' Collection - Navigation Property [1-M Relationship]
        public ICollection<Product> Products { get; set; }

        // Get the SubCategories Collection - Navigation Property [1-M Relationship]
        public ICollection<SubCatGallery> SubCatGallery { get; set; }

    }
}