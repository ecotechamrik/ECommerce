using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BAL.Entities
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryID { get; set; }

        [Required]
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public int CategoryOrder { get; set; }
        public bool IsActive { get; set; }
        public int? SectionID { get; set; }
        public Section Section { get; set; }

        // Get the Products' Collection - Navigation Property [1-M Relationship]
        public ICollection<Product> Products { get; set; }

        // Get the Sub Categories' Collection - Navigation Property [1-M Relationship]
        public ICollection<SubCategory> SubCategories { get; set; }

        // Get the Sub Cat Galleries' Collection - Navigation Property [1-M Relationship]
        public ICollection<SubCatGallery> SubCatGalleries { get; set; }
    }
}