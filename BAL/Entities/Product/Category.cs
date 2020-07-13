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
        //[Column(TypeName = "nvarchar(100)")]
        public string CategoryName { get; set; }

        //[Column(TypeName = "nvarchar(500)")]
        public string Description { get; set; }

        public bool IsActive { get; set; }

        // Get the Products' Collection - Navigation Property [1-M Relationship]
        public ICollection<Product> Products { get; set; }

        // Get the Sub Categories' Collection - Navigation Property [1-M Relationship]
        public ICollection<SubCategory> SubCategories { get; set; }
    }
}
